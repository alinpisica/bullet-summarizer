CREATE TABLE summarization
(
    id serial primary key,
    inbound_email_id int,
    high_level_summary text,
    general_conversational_tone text
);