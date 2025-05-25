CREATE OR REPLACE VIEW view_books AS
SELECT 
  b.title,
  b.isbn,
  string_agg(DISTINCT a.name, ', ')   AS authors,
  string_agg(DISTINCT g.name, ', ')   AS genres,
  bs.name   AS status,
  bc.name   AS condition
FROM books b
  LEFT JOIN authors_books ab ON b.book_id     = ab.book_id
  LEFT JOIN authors       a  ON ab.author_id  = a.authors_id
  LEFT JOIN genres_books  gb ON b.book_id     = gb.book_id
  LEFT JOIN genres        g  ON gb.genre_id   = g.genre_id
  JOIN book_status    bs ON b.status_id    = bs.status_id
  JOIN book_condition bc ON b.condition_id = bc.condition_id
GROUP BY 
  b.title, 
  b.isbn, 
  bs.name, 
  bc.name
;

SELECT * FROM view_books WHERE status ='available';

CREATE OR REPLACE VIEW view_overdue AS
SELECT 
  br.borrowing_id,
  c.customer_id,
  c.name       AS customer,
  b.book_id,
  b.title      AS book,
  br.borrow_date,
  br.due_date
FROM borrowings br
JOIN books      b  ON br.book_id     = b.book_id
JOIN customers  c  ON br.customer_id = c.customer_id
WHERE br.return_date IS NULL
  AND br.due_date    < CURRENT_DATE
;


CREATE OR REPLACE VIEW vw_customer_borrowings AS
SELECT
  br.borrowing_id,
  br.customer_id,
  c.name        AS customer,
  br.book_id,
  b.title       AS book,
  bs.name       AS status,
  bc.name       AS condition,
  br.borrow_date,
  br.due_date,
  br.return_date
FROM borrowings br
JOIN customers      c  ON br.customer_id = c.customer_id
JOIN books          b  ON br.book_id     = b.book_id
JOIN book_status    bs ON b.status_id    = bs.status_id
JOIN book_condition bc ON b.condition_id = bc.condition_id
;

SELECT * 
FROM vw_customer_borrowings
WHERE customer_id = 1
ORDER BY borrow_date DESC;

CREATE OR REPLACE VIEW view_customers AS
SELECT
  c.customer_id,
  c.name,
  c.contact_info,
  c.membership_date,
  COUNT(br.borrowing_id) FILTER (WHERE br.return_date IS NULL) 
    AS active_borrowings
FROM customers c
LEFT JOIN borrowings br 
  ON br.customer_id = c.customer_id
GROUP BY
  c.customer_id,
  c.name,
  c.contact_info,
  c.membership_date;

SELECT * FROM view_customers;
