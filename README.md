This is a submission for the [Postmark Challenge: Inbox Innovators](https://dev.to/challenges/postmark).

## What I Built

Bullet Summarizer is a summarization-as-a-service solution that acts based on email forwarding. The main goal is to drastically reduce the time needed to catch up on lengthy email threads, especially when multiple participants are involved.

The application can be directly integrated into any existing system, leveraging AI capabilities (any LLM can be integrated - ChatGPT has been used for the demo due to ease of implementation). All the data collected can also be stored into a client's databse for future reporting, analysis and tracking.

The core use case consists of saving time and reducing decision fatigue through a clean and structured view on relevant email threads.

## Demo

Any user can forward a thread of emails to the following address: `81ec20c617d2beee13278436d818e91c@inbound.postmarkapp.com`. Due to domain issues and outbound rules, some users may not receive a reply with the summarization. In this case, the safest way to demonstrate the functionalities would be to replicate the runtime into your own environment. In order to replcate locally, fill in the `appsettings.json` file with the Postmark and ChatGPT Keys, together with the connection string.

In order to demonstrate the app's functionalities, I have opted for two different scenarios. Each scenario was based on fictive personas, so all names and examples should be taken with a grain of salt. The initial thread will be attached as a spoiler, in order to minimize the content.

In case of failure when sending the results to the user's email, I have created a simple Metabase dashboard that consists of all the tables from the database. All new entries should be automatically added to the dashboard: `http://91.99.124.116:3000/public/dashboard/fcf3b7ed-2641-45d5-ab6d-acc5598bddfb`.

For hosting I used a small Hetzner instance, directly exposing the Metabase UI and the API.

#### Scenario 1. App Launch - Team discussing bug fixes and timelines

The initial thread of emails:
{% spoiler scenario-1 %}

```
============ Forwarded message ============
From: AlinPisica WebsiteContact <alin.pisica.web.contact@gmail.com>
To: "testmail janedoe"<testmailjanedoe730@gmail.com>
Cc: "Alin Pisica"<contact@alinpisica.com>
Date: Sun, 01 Jun 2025 22:33:22 +0300
Subject: Re: Urgent: FitFit Launch Status – Bugs Still in QA?
============ Forwarded message ============


Will do, Jane.
I’ll prepare a fallback launch plan (Aug 21) with revised Marketing and QA checkpoints just in case. Will share with you tomorrow morning.
Also, I suggest we lock down any non-critical merges starting Monday. Alin, let’s finalize that cutoff list on our 10AM sync tomorrow.
We’ll make this launch happen 💪

Cheers,
 —
 Mike Rays
 Chief Technology Officer
 📞 +1 (555) 224-7765
 ✉️


În dum., 1 iun. 2025 la 22:33, testmail janedoe < mailto:testmailjanedoe730@gmail.com > a scris:

Thanks both, this gives me more confidence.
Mike – let’s get a contingency plan in place anyway. Can you prep a quick internal doc outlining what the “Plan B” would look like if we needed to delay by 5 days?
Alin – great work jumping on these issues. Please send me a short daily update until the weekend. Doesn’t need to be long – just a few bullet points so I can keep stakeholders in the loop.

Thanks,
 —
 Jane Doe
 Chief Executive Officer
 📞 +1 (555) 010-1987
 ✉️


On Sun, Jun 1, 2025 at 10:32 PM Alin Pisica < mailto:contact@alinpisica.com > wrote:

Hi Mike, Jane,
Quick update on my side:
✅ iOS crash: root cause found (Bluetooth permission regression in the new onboarding flow). Fix is in progress, should be in QA before lunch.
🔄 Sync issue: looks like the wearable module sends a malformed timestamp when the phone app is in background mode. I'm working on a patch that auto-corrects it on the mobile side. ETA: end of day.
If we don’t find anything new, both of these will be resolved this week.
Let me know if anything else pops up.

Best,
—
Alin Pisica
Software Developer
📞 +1 (555) 988-2210
✉️


---- On Sun, 01 Jun 2025 22:31:55 +0300 AlinPisica WebsiteContact < mailto:alin.pisica.web.contact@gmail.com > wrote ---


Hi Jane,

I understand the concern. I’ve already gone through the high-priority issues. Out of the 7:

3 are already in review, fix is done.
2 are visual bugs on smaller Android screens – not launch blockers in my opinion.
1 involves syncing between wearable and mobile – that one needs deeper investigation.
1 crash reported on some iOS builds – flagged by QA yesterday.

Alin is working on the iOS crash right now. I’ll let him chime in on the sync issue.
We should be fine for 16th if we stay focused. I don’t recommend postponing yet.

Cheers,
 —
 Mike Rays
 Chief Technology Officer
 📞 +1 (555) 224-7765
 ✉️


În dum., 1 iun. 2025 la 22:31, testmail janedoe < mailto:testmailjanedoe730@gmail.com > a scris:

Hi Mike, Alin,

I’ve just reviewed the QA dashboard this morning and noticed that we still have 7 open bugs marked as “high priority”. With only 15 days until launch, I’m getting worried.

Can we please regroup and assess:

- Which of these are actual blockers?
- What’s realistically going to be fixed before the 16th?
- Do we need to consider pushing back the launch?

Let’s sync before EOD. I’d appreciate a written update before 2PM so I can align with Marketing.

Thanks,
 —
 Jane Doe
 Chief Executive Officer
 📞 +1 (555) 010-1987
 ✉️
```

{% endspoiler %}

Result:

#### Scenario 2. Mortgage approval - Renegotiation of a property

The initial thread of emails:

{% spoiler scenario-2 %}

```
============ Forwarded message ============
From: Alin Pisica <contact@alinpisica.com>
To: "testmail janedoe"<testmailjanedoe730@gmail.com>
Cc: "AlinPisica WebsiteContact"<alin.pisica.web.contact@gmail.com>
Date: Sun, 01 Jun 2025 22:39:34 +0300
Subject: Re: Mortgage Credit Approved – Next Steps for Purchase of Property on Linden Street
============ Forwarded message ============

Hi Jane, Mike,

Thank you both – that’s very generous and reasonable. I’m comfortable covering the additional €2,000 from savings and happy we found a middle ground.
Mike – please move forward with the updated paperwork for €153,000 and send over the revised credit agreement when ready.
Jane – I’ll ask my notary to propose a few slots between 6–9 August.
Excited to get this done!

Cheers,
—
Alin Pisica
📞 +40 766 302 118
✉️

---- On Sun, 01 Jun 2025 22:38:56 +0300 testmail janedoe < mailto:testmailjanedoe730@gmail.com > wrote ---


Hi Mike, Alin,

Thanks for the transparency.
Alin – I understand this has been a process for you, and I don’t want to block your efforts. I’m willing to accept €153,000 and move forward, if we can schedule the notary before August 10th. That way I can align with my own moving timeline.
Let’s finalize this – I’d be happy to sell to you.

Best,
 —
 Jane Popescu
 Property Owner
 📞 +40 723 551 449
 ✉️


On Sun, Jun 1, 2025 at 10:38 PM AlinPisica WebsiteContact < mailto:alin.pisica.web.contact@gmail.com > wrote:

Hi Alin, Jane,

Alin – after a quick internal review, we can approve an additional €5,000, bringing your credit ceiling to €153,000, under the same conditions (interest rate and term). However, you would need to cover the remaining €2,000 difference through your own contribution.

Jane – would you consider splitting the difference with Alin to help close the sale? Perhaps meeting in the middle could make this work for both sides.

Let me know your thoughts so we can move quickly.

Regards,
 —
 Mike Rays
 Loan Officer – Residential Mortgages
 FirstBank Europe
 📞 +40 745 882 220
 ✉️


În dum., 1 iun. 2025 la 22:38, Alin Pisica < mailto:contact@alinpisica.com > a scris:

Hi Jane, Mike,

Jane – I respect your position, but this comes as a surprise. We agreed on €148,000 and I’ve already paid for valuation, legal checks, and initiated paperwork with the bank based on that amount.

Mike – Is there any way the bank can increase the approved amount to match the new price? Otherwise, I’d be forced to withdraw.

I still really want the house, but I’m already stretched with my budget.

Thanks,
—
Alin Pisica
📞 +40 766 302 118
✉️

---- On Sun, 01 Jun 2025 22:37:30 +0300 AlinPisica WebsiteContact < mailto:alin.pisica.web.contact@gmail.com > wrote ---

Dear Jane, Alin,

I’m pleased to inform you that the credit application for Mr. Alin Pisica has been officially approved. The credit covers the initially agreed price of €148,000 for the property located at 24 Linden Street.

Next steps:

We can begin preparing the pre-contract.
The notary appointment can be set once Jane confirms availability.
Please confirm if there are any last-minute changes to the agreed terms.

Looking forward to your confirmation to proceed.

Kind regards,
 —
 Mike Rays
 Loan Officer – Residential Mortgages
 FirstBank Europe
 📞 +40 745 882 220
 ✉️
```

{%endspoiler%}

Result:

## Code Repository

The code is structured using a 3 layered architecture, split into three projects: API (that acts as the main app - exposing the API and using the background workers), Core (that hosts all the domain logic, models, types) and Infrastructure (the communications, integrations and processes).

The database migrations are created using DbUp, opting for a simple schema regarding the storage of all the results. No foreign keys or indexes were used in order to keep the system simple for demo purposes, easy to reproduce/run locally and avoid any possible issue regarding database optimizations. All the database migrations are located under Infrastructure/Repository/Database/Scripts. The queries and database access is simplified by using Dapper.

In order to host it on linux as a service, utilities are included into the utils folder (bash scripts for starting, stoping and checking the service's status, together with the service template to run under systemctl).

## How I Built It

### Tech Stack

Tech Stack used:

-   .NET 8 for the API, processing and orchestration
-   PostgreSQL for storing the inbound emails and summarization results (action items, bullet points, etc.)
-   Structured Chat GPT for the NLP management, understanding and extracting information
-   Metabase for dashboard visualisation

### Architecture

The architecture consists of a .NET app that has the following three functionalities:

-   a publicly exposed API endpoint that is used as a webhook for Postmark's connectivity
-   a background worker that retrieves unprocessed emails from the database and stores them into an in-memory queue
-   a background worker that retrieves data from the in-memory queue, computes the summarization, saves the results into database and sends the email to the user

### Structured GPT Response

In order to easily obtain a consistent response, I have leveraged the functionality of structured answering of ChatGPT. In this way, every response is returned using a stable data structure that can be easily interpreted and mapped to our needs.

The response schema is the following and can be easily expanded and adapted based on new features:

```
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
```

### Upcoming features

Since I loved working with Postmark, I am thinking of extending the current product with the following functionality:

-   update the reasoning service to easily integrate with multiple LLM providers
-   integrate directly with Google Sheets and Google Tasks to directly upload and manage data based on email interactions
-   create multiple forwarding rules and differential results for all participants (each thread participant will receive its own copy of the summarization but only with its personal interests)
-   add follow up functionalities - the system should follow up on previous summarizations to make sure that all the action items have been completed
