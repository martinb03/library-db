CREATE OR REPLACE FUNCTION add_book(
  p_title        VARCHAR,
  p_isbn         VARCHAR,
  p_status_id    INT    DEFAULT (
    SELECT status_id FROM book_status WHERE name = 'available'
  ),
  p_condition_id INT    DEFAULT (
    SELECT condition_id FROM book_condition WHERE name = 'new'
  )
) RETURNS INT AS $$
DECLARE
  v_book_id INT;
BEGIN
  INSERT INTO books (title, isbn, status_id, condition_id)
  VALUES (p_title, p_isbn, p_status_id, p_condition_id)
  RETURNING book_id INTO v_book_id;
  RETURN v_book_id;
END;
$$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION add_customer(
  p_name         VARCHAR,
  p_contact_info VARCHAR
) RETURNS INT AS $$
DECLARE
  v_customer_id INT;
BEGIN
  INSERT INTO customers (name, contact_info)
  VALUES (p_name, p_contact_info)
  RETURNING customer_id INTO v_customer_id;
  RETURN v_customer_id;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION update_customer(
  p_customer_id  INT,
  p_name         VARCHAR,
  p_contact_info VARCHAR
) RETURNS VOID AS $$
BEGIN
  UPDATE customers
  SET
    name         = p_name,
    contact_info = p_contact_info
  WHERE customer_id = p_customer_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Customer % not found', p_customer_id;
  END IF;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION delete_customer(p_customer_id INT) RETURNS VOID AS $$
DECLARE
  v_active_loans INT;
BEGIN
  SELECT COUNT(*) INTO v_active_loans
    FROM borrowings
    WHERE customer_id = p_customer_id
      AND return_date IS NULL;

  IF v_active_loans > 0 THEN
    RAISE EXCEPTION 'Cannot delete customer %: % active borrowings',
      p_customer_id, v_active_loans;
  END IF;

  DELETE FROM customers WHERE customer_id = p_customer_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Customer % not found', p_customer_id;
  END IF;
END;
$$ LANGUAGE plpgsql;
