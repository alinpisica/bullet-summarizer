using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.Text.Json;

namespace BulletSummarizer.Infrastructure.Services
{
    public class ReasoningService : IReasoningService
    {
        private readonly ChatClient _chatClient;

        private readonly SystemChatMessage _systemChatMessage;
        private readonly ChatResponseFormat _chatResponseFormat;

        public ReasoningService(IConfiguration configuration)
        {
            IConfigurationSection reasoningServiceConfiguration = configuration.GetSection("ReasoningService");

            _chatResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "email_bullet_summarization",
                jsonSchema: BinaryData.FromString("""
                    {
                        "type": "object",
                        "properties": {
                            "highLevelSummary": {
                                "type": "string"
                            },
                            "generalConversationalTone": {
                                "type": "string"
                            },
                            "participants": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "name": { "type": "string" },
                                        "email": { "type": "string" },
                                        "role": { "type": "string" }
                                    },
                                    "required": ["name", "email", "role"],
                                    "additionalProperties": false
                                }
                            },
                            "actionItems": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "title": { "type": "string" },
                                        "description": { "type": "string" },
                                        "assignee": { "type": "string" },
                                        "dueDate": { "type": "string" }
                                    },
                                    "required": ["title", "description", "assignee", "dueDate"],
                                    "additionalProperties": false
                                }
                            },
                            "decisions": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "content": { "type": "string" },
                                        "decidedBy": { "type": "string" },
                                        "date": { "type": "string" }
                                    },
                                    "required": ["content", "decidedBy", "date"],
                                    "additionalProperties": false
                                }
                            },
                            "keyDatesAndDeadlines": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "label": { "type": "string" },
                                        "date": { "type": "string" }
                                    },
                                    "required": ["label", "date"],
                                    "additionalProperties": false
                                }
                            },
                            "bullets": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "content": { "type": "string" },
                                        "relatedTo": { "type": "string" }
                                    },
                                    "required": ["content", "relatedTo"],
                                    "additionalProperties": false
                                }
                            }
                        },
                        "required": ["highLevelSummary", "generalConversationalTone", "participants", "actionItems", "decisions", "keyDatesAndDeadlines", "bullets"],
                        "additionalProperties": false
                    }
                    """),
                jsonSchemaIsStrict: true);


            _chatClient = new(reasoningServiceConfiguration["Model"], reasoningServiceConfiguration["API_KEY"]);

            _systemChatMessage = new("You are a smart email summarizer. You will receive an email or a thread of emails and you will extract a short bullet summarization together with possible action items from the emails. The bullets should be short but explainative. You will always respond in english.");
        }

        public Summarization? SummarizeContent(string content)
        {
            UserChatMessage userChatMessage = new(content);

            var data = _chatClient.CompleteChat([_systemChatMessage, userChatMessage], new()
            {
                ResponseFormat = _chatResponseFormat
            });

            return JsonSerializer.Deserialize<Summarization>(data.Value.Content[0].Text);
        }
    }
}
