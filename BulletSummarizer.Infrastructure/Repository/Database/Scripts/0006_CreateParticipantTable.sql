CREATE TABLE participant
(
    id serial primary key,
    summarization_id int,
    name text,
    email text,
    role text
);