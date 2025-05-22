INSERT INTO books (title, isbn, status_id, condition_id) VALUES
('The Art of War', '9780000000001', 1, 1),
('IT', '9780000000002', 1, 2),
('Harry Potter and the Sorcerer''s Stone', '9780000000003', 1, 1),
('The Great Gatsby', '9780000000004', 1, 2),
('A Brief History of Time', '9780000000005', 2, 2),
('Long Walk to Freedom', '9780000000006', 1, 3);

INSERT INTO authors (name) VALUES
('Sun Tzu'),
('Stephen King'),
('J.K. Rowling'),
('F. Scott Fitzgerald'),
('Stephen Hawking'),
('Nelson Mandela');

INSERT INTO authors_books (author_id, book_id, is_main_author) VALUES
(1, 1, TRUE),  -- Sun Tzu → Art of War
(2, 2, TRUE),  -- Stephen King → IT
(3, 3, TRUE),  -- Rowling → HP
(4, 4, TRUE),  -- Fitzgerald → Gatsby
(5, 5, TRUE),  -- Hawking → Time
(6, 6, TRUE);  -- Mandela → Freedom;


INSERT INTO genres_books (genre_id, book_id, is_main_genre) VALUES
(3, 1, TRUE),  -- Art of War → Philosophy
(5, 1, FALSE), -- Art of War → History
(10, 2, TRUE),  -- IT → Horror
(6, 3, TRUE),  -- HP → Fantasy
(1, 4, TRUE),  -- Gatsby → Fiction
(2, 5, TRUE),  -- Brief History → Science
(3, 6, FALSE), -- Long Walk → History
(4, 6, TRUE);  -- Long Walk → Biography

INSERT INTO customers (name, contact_info, membership_date) VALUES
('Alice Johnson', 'alice.johnson@email.com', '2023-01-15'),
('Bob Smith', 'bob.smith@email.com', '2023-03-22'),
('Clara Martinez', 'clara.martinez@email.com', '2024-06-01');


-- Alice borrows "IT" and has returned it
INSERT INTO borrowings (book_id, customer_id, borrow_date, due_date, return_date)
VALUES (2, 1, '2024-04-01', '2024-04-21', '2024-04-20');

-- Bob borrows "A Brief History of Time", currently overdue
INSERT INTO borrowings (book_id, customer_id, borrow_date, due_date)
VALUES (5, 2, '2024-04-15', '2024-04-25');

INSERT INTO staff ("user", password_hash, is_admin) VALUES
('admin', 'admin', TRUE),
('librarian1', 'lib1', FALSE),
('librarian2', 'lib2', FALSE);
