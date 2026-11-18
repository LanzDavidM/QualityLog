DROP TABLE IF EXISTS inspection;

CREATE TABLE inspection (
    id           SERIAL PRIMARY KEY,
    part_number  TEXT NOT NULL,
    date         DATE NOT NULL,
    shift        TEXT NOT NULL,
    inspector    TEXT NOT NULL,
    result       TEXT NOT NULL,
    defect_type  TEXT
);

INSERT INTO inspection (part_number, date, shift, inspector, result, defect_type) VALUES
    ('A-1001', '2026-11-02', 'First',  'Smith', 'Pass',   NULL),
    ('A-1002', '2026-11-02', 'First',  'Smith', 'Fail',   'Burr'),
    ('B-2001', '2026-11-03', 'Second', 'Jones', 'Rework', 'Weld porosity'),
    ('B-2002', '2026-11-03', 'Second', 'Jones', 'Pass',   NULL);
