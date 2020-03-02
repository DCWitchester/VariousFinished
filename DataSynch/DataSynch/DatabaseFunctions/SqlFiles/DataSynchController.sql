CREATE DATABASE "DataSynchController" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';

ALTER DATABASE "DataSynchController" OWNER TO postgres;

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

-- #region Table Creation --
-- #region Clienti--
CREATE TABLE public.clienti (
    id SERIAL NOT NULL PRIMARY KEY,
    guid_client VARCHAR NOT NULL DEFAULT '',
    cod_fiscal VARCHAR NOT NULL DEFAULT '',
    denumire VARCHAR NOT NULL DEFAULT '',
    punct_lucru_id INTEGER NOT NULL DEFAULT 0
);

ALTER TABLE public.clienti OWNER TO postgres;
-- #endregion Clienti --
-- #region Punct Lucru --
CREATE TABLE public.puncte_lucru (
    id SERIAL NOT NULL PRIMARY KEY,
    guid_punct_lucru VARCHAR NOT NULL DEFAULT '',
    client_id INTEGER NOT NULL DEFAULT 0 REFERENCES public.clienti(id),
    server_lan_ip VARCHAR NOT NULL DEFAULT '',
    server_wan_ip VARCHAR NOT NULL DEFAULT '',
    database_connection_string VARCHAR NOT NULL DEFAULT '',
    server_mac_adress VARCHAR NOT NULL DEFAULT '',
    server_memo_file TEXT NOT NULL DEFAULT '',
    last_memo_update TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT ('now'::text)::timestamp without time zone,
    file_path VARCHAR NOT NULL DEFAULT '',
    cod_punct_lucru VARCHAR NOT NULL DEFAULT '0',
    denumire VARCHAR NOT NULL DEFAULT ''
);

ALTER TABLE public.puncte_lucru OWNER TO postgres;
-- #endregion Punct Lucru --

-- #region Setari Punct Lucru --
CREATE TABLE public.setari_puncte_lucru (
    id SERIAL NOT NULL PRIMARY KEY,
    punct_lucru_id INTEGER NOT NULL DEFAULT 0 REFERENCES public.puncte_lucru(id) UNIQUE,
    retrieve_from_server BOOLEAN NOT NULL DEFAULT false,
    retrieved_id_list VARCHAR NOT NULL DEFAULT '', 
    retrieve_documents BOOLEAN NOT NULL DEFAULT false,
    retrieve_nomenclators BOOLEAN NOT NULL DEFAULT false,
    retrieve_specific BOOLEAN NOT NULL DEFAULT false,
    specific_file_list VARCHAR NOT NULL DEFAULT '',
    is_server BOOLEAN NOT NULL DEFAULT false
);

ALTER TABLE public.setari_puncte_lucru OWNER TO postgres;
-- #endregion Setari Punct Lucru --
-- #region Setari Client --
CREATE TABLE public.setari_client (
    id SERIAL NOT NULL PRIMARY KEY,
    client_id INTEGER NOT NULL DEFAULT 0 REFERENCES public.clienti(id) UNIQUE,
    display_mesaj BOOLEAN NOT NULL DEFAULT false,
    mesaj VARCHAR NOT NULL DEFAULT '',
    blocat BOOLEAN NOT NULL DEFAULT false
);

ALTER TABLE public.setari_client OWNER TO postgres;
-- #endregion Setari Client --
-- #endregion Table Creation --

-- #region Initial Inserts --
INSERT INTO public.clienti(id,denumire) VALUES (0,'NONE');
INSERT INTO public.puncte_lucru(id,cod_punct_lucru,denumire) VALUES(0,'0','NONE');
-- #endregion Initial Inserts --

-- #region Foreign Key Fix --
ALTER TABLE public.clienti ADD CONSTRAINT main_wp_fk FOREIGN KEY (punct_lucru_id) REFERENCES public.puncte_lucru(id);
-- #endregion Foreign Key Fix --