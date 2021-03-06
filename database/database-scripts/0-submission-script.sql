CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE works_deadlines (
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    link uuid NOT NULL,
    submission_start timestamp with time zone NOT NULL,
    submission_end timestamp with time zone NOT NULL,
    CONSTRAINT "PK_works_deadlines" PRIMARY KEY (id)
);

CREATE TABLE works (
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    content text NULL,
    submitted timestamp with time zone NULL,
    modified timestamp with time zone NULL,
    student_id integer NOT NULL,
    works_deadline_id integer NOT NULL,
    CONSTRAINT "PK_works" PRIMARY KEY (id),
    CONSTRAINT "FK_works_works_deadlines_works_deadline_id" FOREIGN KEY (works_deadline_id) REFERENCES works_deadlines (id) ON DELETE CASCADE
);

CREATE INDEX "IX_works_works_deadline_id" ON works (works_deadline_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210522183740_InitialModel', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE works RENAME COLUMN student_id TO author_id;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210523134721_RenameStudentIdToAuthorIdInWorkTable', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE works_deadlines RENAME COLUMN link TO "Uid";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210523145033_RenameLinkToUidInWorksDeadlinesTable', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE works DROP CONSTRAINT "FK_works_works_deadlines_works_deadline_id";

DROP TABLE works_deadlines;

ALTER TABLE works DROP CONSTRAINT "PK_works";

ALTER TABLE works RENAME TO submissions;

ALTER TABLE submissions RENAME COLUMN works_deadline_id TO submissions_deadline_id;

ALTER INDEX "IX_works_works_deadline_id" RENAME TO "IX_submissions_submissions_deadline_id";

ALTER TABLE submissions ADD CONSTRAINT "PK_submissions" PRIMARY KEY (id);

CREATE TABLE submissions_deadlines (
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    uid uuid NOT NULL,
    submission_start timestamp with time zone NOT NULL,
    submission_end timestamp with time zone NOT NULL,
    CONSTRAINT "PK_submissions_deadlines" PRIMARY KEY (id)
);

ALTER TABLE submissions ADD CONSTRAINT "FK_submissions_submissions_deadlines_submissions_deadline_id" FOREIGN KEY (submissions_deadline_id) REFERENCES submissions_deadlines (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210608153615_RenameWorksToSubmissions', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE submissions DROP CONSTRAINT "FK_submissions_submissions_deadlines_submissions_deadline_id";

DROP TABLE submissions_deadlines;

ALTER TABLE submissions RENAME COLUMN submissions_deadline_id TO submission_deadlines_id;

ALTER INDEX "IX_submissions_submissions_deadline_id" RENAME TO "IX_submissions_submission_deadlines_id";

ALTER TABLE submissions ALTER COLUMN author_id TYPE text;
ALTER TABLE submissions ALTER COLUMN author_id DROP NOT NULL;

CREATE TABLE submission_deadlines (
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    uid uuid NOT NULL,
    submission_start timestamp with time zone NOT NULL,
    submission_end timestamp with time zone NOT NULL,
    CONSTRAINT "PK_submission_deadlines" PRIMARY KEY (id)
);

ALTER TABLE submissions ADD CONSTRAINT "FK_submissions_submission_deadlines_submission_deadlines_id" FOREIGN KEY (submission_deadlines_id) REFERENCES submission_deadlines (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210609143026_ModifySubmissionDeadlines', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE submission_deadlines ADD instructor_id character varying(255) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210610192304_AddInstructorIdColumn', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE submissions ADD author character varying(255) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210710172928_AddAuthorColumnToParticipantsTable', '5.0.6');

COMMIT;

START TRANSACTION;

ALTER TABLE submissions DROP CONSTRAINT "FK_submissions_submission_deadlines_submission_deadlines_id";

DROP INDEX "IX_submissions_submission_deadlines_id";

ALTER TABLE submissions DROP COLUMN submission_deadlines_id;

ALTER TABLE submissions ADD workshop_uid uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210712141053_AddWorkshopUidColumnToSubmissions', '5.0.6');

COMMIT;

