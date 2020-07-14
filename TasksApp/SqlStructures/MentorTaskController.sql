-- Database is made in PGSQL 11
ALTER DATABASE "MentorTaskController" OWNER TO postgres;

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

--REGION utilizatori
CREATE TABLE public.utilizatori
(
	id SERIAL NOT NULL PRIMARY KEY,
	nume_utilizator  VARCHAR NOT NULL DEFAULT '',
	parola VARCHAR NOT NULL DEFAULT '',
	display_name VARCHAR NOT NULL DEFAULT '',
	email VARCHAR NOT NULL DEFAULT '' UNIQUE,
	color_code VARCHAR NOT NULL DEFAULT '',
	nivel INTEGER NOT NULL DEFAULT 0,
	is_logged BOOLEAN NOT NULL DEFAULT false,
	activ BOOLEAN NOT NULL DEFAULT true,
	UNIQUE(nume_utilizator,parola),
	UNIQUE(email)
);

COMMENT ON TABLE public.utilizatori IS 'Table was made for ease of scalability in the future and ease of control';
--Color_code va fi folosit intr-o versiune viitoare pentru a separa sau marca unii utilizatori
COMMENT ON COLUMN public.utilizatori.color_code IS 'For future use...';
--Nivel va oferi in versiuni urmatoare control: ex admin, base, team_leader
COMMENT ON COLUMN public.utilizatori.nivel IS 'For future use...';
COMMENT ON COLUMN public.utilizatori.is_logged IS 'For future use...';
--ENDREGION utilizatori

--REGION task_status
CREATE TABLE public.task_status
(
	id SERIAL NOT NULL PRIMARY KEY,
	status_code VARCHAR NOT NULL DEFAULT '' UNIQUE,
	status_text VARCHAR NOT NULL DEFAULT '',
	status_color VARCHAR NOT NULL DEFAULT ''
);
--Will most likely implement this in later versions, depending how much it will take
COMMENT ON TABLE public.task_status IS 'Table will be used for linking status codes to independent tasks';

--REGION tasks
CREATE TABLE public.tasks
(
	id SERIAL NOT NULL PRIMARY KEY,
	task_code VARCHAR NOT NULL DEFAULT '',
	task VARCHAR NOT NULL DEFAULT '',
	creator INTEGER NOT NULL DEFAULT 0 REFERENCES public.utilizatori(id) ON UPDATE CASCADE ON DELETE CASCADE, 
	parent_task INTEGER NOT NULL DEFAULT 0 REFERENCES public.tasks(id) ON UPDATE CASCADE ON DELETE CASCADE,
	task_status INTEGER NOT NULL DEFAULT 0 REFERENCES public.task_status(id),
	is_public BOOLEAN NOT NULL DEFAULT false,
	activ BOOLEAN NOT NULL DEFAULT true
);


COMMENT ON TABLE public.tasks IS 'The central table for the database structure';
COMMENT ON COLUMN public.tasks.task_code IS 'Auto or NonAuto generated code for user display';
--Initialy tasks will be shown only to the person who registered it
--When the command level will be initialized it will be visible to him, to admins, and to direct leaders
--The leader will be able to asign task to his subordinates
COMMENT ON COLUMN public.tasks.creator IS 'The person who initialy registered the task';
COMMENT ON COLUMN public.tasks.is_public IS 'Public Tasks will ALWAYS be seen by everyone';
--ENDREGION tasks

CREATE TABLE public.shared_tasks(
	id SERIAL NOT NULL PRIMARY KEY,
	task_id INTEGER NOT NULL DEFAULT 0 REFERENCES public.tasks(id),
	shared_with INTEGER NOT NULL DEFAULT 0 REFERENCES public.utilizatori(id),
	activ BOOLEAN NOT NULL DEFAULT true
);
--ENDREGION