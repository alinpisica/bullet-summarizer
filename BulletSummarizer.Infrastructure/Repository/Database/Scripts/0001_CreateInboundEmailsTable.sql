CREATE TABLE inbound_email
(
    id serial primary key,
    content text not null,
    summarization_id int default null,
    retry_count int not null default 0,
    queue_entry_time timestamp with time zone
);