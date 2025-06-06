CREATE TABLE bullet
(
    id serial primary key,
    summarization_id int,
    content text,
    related_to text
);