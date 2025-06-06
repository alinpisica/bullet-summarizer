CREATE TABLE decision
(
    id serial primary key,
    summarization_id int,
    content text,
    decided_by text,
    date text
);