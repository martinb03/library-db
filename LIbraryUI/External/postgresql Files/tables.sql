BEGIN;


CREATE TABLE IF NOT EXISTS public.books
(
    book_id serial,
    title character varying(255) NOT NULL,
    isbn character varying(20),
    status_id integer NOT NULL,
    condition_id integer NOT NULL,
    PRIMARY KEY (book_id),
    UNIQUE (isbn)
);


ALTER TABLE IF EXISTS public.books
    ADD CONSTRAINT books_status_id_fkey
    FOREIGN KEY (status_id) REFERENCES public.book_status (status_id);


ALTER TABLE IF EXISTS public.books
    ADD CONSTRAINT books_condition_id_fkey
	FOREIGN KEY (condition_id) REFERENCES public.book_condition (condition_id);



CREATE TABLE IF NOT EXISTS public.genres
(
    genre_id serial,
    name character varying(255) NOT NULL,
    PRIMARY KEY (genre_id),
    UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.book_status
(
    status_id serial,
    name character varying(50) NOT NULL,
    PRIMARY KEY (status_id),
    UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.customers
(
    customer_id serial,
    name character varying(255) NOT NULL,
    contact_info character varying(255) NOT NULL,
    membership_date date DEFAULT CURRENT_DATE,
    PRIMARY KEY (customer_id)
);

CREATE TABLE IF NOT EXISTS public.authors
(
    authors_id serial,
    name character varying(255) NOT NULL,
    PRIMARY KEY (authors_id),
    UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS public.borrowings
(
    borrowing_id serial,
    book_id integer REFERENCES books(book_id),
    customer_id integer REFERENCES customers(customer_id) ON DELETE CASCADE,
    borrow_date date NOT NULL DEFAULT CURRENT_DATE,
    due_date date NOT NULL,
    return_date date NOT NULL,
    PRIMARY KEY (borrowing_id)
);

ALTER TABLE borrowings
ADD CONSTRAINT borrowings_book_id_fkey
FOREIGN KEY (book_id) REFERENCES books(book_id);

ALTER TABLE borrowings
ADD CONSTRAINT borrowings_customer_id_fkey
FOREIGN KEY (customer_id) REFERENCES customers (customer_id) ON DELETE CASCADE; 

ALTER TABLE borrowings
ALTER COLUMN return_date DROP NOT NULL;

CREATE TABLE IF NOT EXISTS public.book_condition
(
    condition_id serial,
    name character varying(255) NOT NULL,
    PRIMARY KEY (condition_id),
	UNIQUE(name)
);



CREATE TABLE IF NOT EXISTS public.authors_books
(
    author_id integer REFERENCES authors(authors_id) ON DELETE CASCADE,
    book_id integer REFERENCES books(book_id) ON DELETE CASCADE,
	is_main_author boolean DEFAULT FALSE,
    PRIMARY KEY (author_id, book_id)
);

CREATE TABLE IF NOT EXISTS public.genres_books
(
    genre_id integer REFERENCES genres(genre_id) ON DELETE CASCADE,
    book_id integer REFERENCES books(book_id) ON DELETE CASCADE,
	is_main_genre boolean DEFAULT FALSE,
    PRIMARY KEY (genre_id, book_id)
);

CREATE TABLE IF NOT EXISTS public.penalties
(
    penalty_id serial,
    type character varying(20) CHECK (type IN ('late_fee', 'replacement_fee')) NOT NULL,
    borrowing_id integer REFERENCES borrowings(borrowing_id) ON DELETE CASCADE,
    amount numeric(6, 2) NOT NULL,
    reason text,
    issue_date date NOT NULL DEFAULT CURRENT_DATE,
    PRIMARY KEY (penalty_id)
);

CREATE TABLE IF NOT EXISTS public.staff
(
    staff_id serial,
    "user" character varying(100) NOT NULL,
    password_hash text NOT NULL,
    is_admin boolean NOT NULL DEFAULT FALSE,
    PRIMARY KEY (staff_id),
    UNIQUE ("user")
);




END;