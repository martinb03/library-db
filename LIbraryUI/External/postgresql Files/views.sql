CREATE OR REPLACE VIEW view_book AS
SELECT 
  b.book_id,
  b.title,
  b.isbn,
  bs.name   AS status,
  bc.name   AS condition,
  string_agg(DISTINCT a.name, ', ')   AS authors,
  string_agg(DISTINCT g.name, ', ')   AS genres
FROM books b
  JOIN book_status    bs ON b.status_id    = bs.status_id
  JOIN book_condition bc ON b.condition_id = bc.condition_id
  LEFT JOIN authors_books ab ON b.book_id     = ab.book_id
  LEFT JOIN authors       a  ON ab.author_id  = a.authors_id
  LEFT JOIN genres_books  gb ON b.book_id     = gb.book_id
  LEFT JOIN genres        g  ON gb.genre_id   = g.genre_id
GROUP BY 
  b.book_id, 
  b.title, 
  b.isbn, 
  bs.name, 
  bc.name
;


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

SELECT * FROM view_book;

