CREATE OR REPLACE FUNCTION set_status_borrowed()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE books
    SET status_id = 2  -- assumes 2 = 'borrowed'
    WHERE book_id = NEW.book_id;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_status_borrowed	--updates book's status when a borrowing for it is created
AFTER INSERT ON borrowings
FOR EACH ROW
EXECUTE FUNCTION set_status_borrowed();



CREATE OR REPLACE FUNCTION set_status_available()
RETURNS TRIGGER AS $$
BEGIN
    -- Only run if return_date was previously NULL and is now NOT NULL
    IF NEW.return_date IS NOT NULL AND OLD.return_date IS NULL THEN
        UPDATE books
        SET status_id = 1  -- 1 = 'available'
        WHERE book_id = NEW.book_id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;



CREATE TRIGGER trg_set_book_status_available
AFTER UPDATE ON borrowings
FOR EACH ROW
WHEN (OLD.return_date IS DISTINCT FROM NEW.return_date)
EXECUTE FUNCTION set_status_available();




CREATE OR REPLACE FUNCTION set_unusable_damaged()
RETURNS TRIGGER AS $$
DECLARE
  unusable_id INT;
  damaged_id  INT;
BEGIN
  -- look up the IDs for clarity rather than hard-coding
  SELECT condition_id INTO unusable_id
    FROM book_condition WHERE name = 'unusable';
  SELECT status_id INTO damaged_id
    FROM book_status    WHERE name = 'damaged';

  IF NEW.condition_id = unusable_id THEN
    UPDATE books
    SET status_id = damaged_id
    WHERE book_id = NEW.book_id;
  END IF;

  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_unusable_damaged
AFTER UPDATE OF condition_id ON books
FOR EACH ROW
EXECUTE FUNCTION set_unusable_damaged();
