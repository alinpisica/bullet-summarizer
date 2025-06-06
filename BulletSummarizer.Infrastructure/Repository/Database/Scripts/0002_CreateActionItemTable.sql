CREATE TABLE action_item
(
    id serial primary key,
    summarization_id int,
    title text,
    description text,
    assignee text,
    due_date text
);