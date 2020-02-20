-- Started on 2020-01-23 14:22:45

--SET statement_timeout = 0;
--SET lock_timeout = 0;
--SET idle_in_transaction_session_timeout = 0;
--SET client_encoding = 'UTF8';
--SET standard_conforming_strings = on;
--SELECT pg_catalog.set_config('search_path', '', false);
--SET check_function_bodies = false;
--SET xmloption = content;
--SET client_min_messages = warning;
--SET row_security = off;

--DROP DATABASE "<DatabaseName>";
--
-- TOC entry 3943 (class 1262 OID 88877)
-- Name: <DatabaseName>; Type: DATABASE; Schema: -; Owner: postgres
--

ALTER DATABASE "<DatabaseName>" OWNER TO postgres;

--\connect "<DatabaseName>"

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

--
-- TOC entry 223 (class 1259 OID 89444)
-- Name: agenti; Type: TABLE; Schema: public; Owner: postgres
--
-- region: Agenti
CREATE TABLE public.agenti (
    id integer NOT NULL,
    local_id integer NOT NULL,
    agent character varying(10) DEFAULT ''::character varying NOT NULL,
    tip character varying(10) DEFAULT ''::character varying NOT NULL,
    nume character varying(25) DEFAULT ''::character varying NOT NULL,
    procent numeric(6,3) DEFAULT 0 NOT NULL,
    bi character varying(12) DEFAULT ''::character varying NOT NULL,
    bidata date DEFAULT ('now'::text)::date NOT NULL,
    cnp character varying(13) DEFAULT ''::character varying NOT NULL,
    ss character varying(20) DEFAULT ''::character varying NOT NULL,
    sm character varying(20) DEFAULT ''::character varying NOT NULL,
    masina character varying(20) DEFAULT ''::character varying NOT NULL,
    caleps character varying(50) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    zona character varying(20) DEFAULT ''::character varying NOT NULL,
    chitanta numeric(10,0) DEFAULT 0 NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments on agenti table
COMMENT ON TABLE public.agenti IS 'Echivalent agenti.dbf <= Nomenclator Agenti <= Fisier Unic';
COMMENT ON COLUMN public.agenti.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.agenti.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.agenti.retrieved IS 'The column used to synch data downward';
--

ALTER TABLE public.agenti OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 89442)
-- Name: agenti_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.agenti_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.agenti_id_seq OWNER TO postgres;

--
-- TOC entry 3944 (class 0 OID 0)
-- Dependencies: 222
-- Name: agenti_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.agenti_id_seq OWNED BY public.agenti.id;

-- endregion Agenti

--
-- TOC entry 257 (class 1259 OID 89839)
-- Name: avize; Type: TABLE; Schema: public; Owner: postgres
--

-- region Avize
CREATE TABLE public.avize (
    id integer NOT NULL,
    local_id integer NOT NULL,
    nid numeric(10,0) DEFAULT 0 NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    agent character varying(10) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    cantfact numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    procad numeric(6,2) DEFAULT 0 NOT NULL,
    pv2 numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

-- Comments on avize table
COMMENT ON TABLE public.avize IS 'Echivalent avize.dbf <= Document Avize <= Fisier Unic';
COMMENT ON COLUMN public.avize.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.avize.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.avize.retrieved IS 'The column used to synch data downward';
--



ALTER TABLE public.avize OWNER TO postgres;

--
-- TOC entry 256 (class 1259 OID 89837)
-- Name: avize_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.avize_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.avize_id_seq OWNER TO postgres;

--
-- TOC entry 3945 (class 0 OID 0)
-- Dependencies: 256
-- Name: avize_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.avize_id_seq OWNED BY public.avize.id;

-- endregion Avize

--
-- TOC entry 205 (class 1259 OID 89163)
-- Name: bonuri_consum; Type: TABLE; Schema: public; Owner: postgres
--

-- region Bonuri Consum
CREATE TABLE public.bonuri_consum (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    contg character varying(8) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

-- Comments on bonuri consum table
COMMENT ON TABLE public.bonuri_consum IS 'Echivalent bcmmyyyy.dbf <= Document Bonuri Consum <= Fisier Lunar';
COMMENT ON COLUMN public.bonuri_consum.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.bonuri_consum.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.bonuri_consum.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.bonuri_consum.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.bonuri_consum.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.bonuri_consum OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 89161)
-- Name: bonuri_consum_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bonuri_consum_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bonuri_consum_id_seq OWNER TO postgres;

--
-- TOC entry 3946 (class 0 OID 0)
-- Dependencies: 204
-- Name: bonuri_consum_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bonuri_consum_id_seq OWNED BY public.bonuri_consum.id;

-- endregion Bonuri Consum

--
-- TOC entry 243 (class 1259 OID 89640)
-- Name: comandax; Type: TABLE; Schema: public; Owner: postgres
--

-- region Comandax
--#warning TBD: COMANDAX SE LEAGA DE FOLDERUL PSM <= Multiple fisiere facute pe aceeasi structura
CREATE TABLE public.comandax (
    id integer NOT NULL,
    pac character varying(4) DEFAULT ''::character varying NOT NULL,
    idcom numeric(10,0) DEFAULT 0 NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    agent character varying(10) DEFAULT ''::character varying NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    achit character varying(40) DEFAULT ''::character varying NOT NULL,
    delegat character varying(25) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(15) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    prad numeric(10,4) DEFAULT 0 NOT NULL,
    discount numeric(6,2) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    valv numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    lot character varying(50) DEFAULT ''::character varying NOT NULL,
    reteta character varying(30) DEFAULT ''::character varying NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.comandax OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 89638)
-- Name: comandax_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.comandax_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comandax_id_seq OWNER TO postgres;

--
-- TOC entry 3947 (class 0 OID 0)
-- Dependencies: 242
-- Name: comandax_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.comandax_id_seq OWNED BY public.comandax.id;

-- endregion Comandax

--
-- TOC entry 229 (class 1259 OID 89488)
-- Name: comenzi; Type: TABLE; Schema: public; Owner: postgres
--

-- region Comenzi

CREATE TABLE public.comenzi (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    gestutil character varying(4) DEFAULT ''::character varying NOT NULL,
    culoare character varying(20) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    stoc numeric(13,4) DEFAULT 0 NOT NULL,
    descriere character varying(100) DEFAULT ''::character varying NOT NULL,
    datarecept date DEFAULT ('now'::text)::date NOT NULL,
    stare numeric(10,0) DEFAULT 0 NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

COMMENT ON TABLE public.comenzi IS 'Echivalent comenzi.dbf <= Document Comenzi <= Fisier Unic';
COMMENT ON COLUMN public.comenzi.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.comenzi.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.comenzi.retrieved IS 'The column used to synch data downward';


ALTER TABLE public.comenzi OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 89486)
-- Name: comenzi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.comenzi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comenzi_id_seq OWNER TO postgres;

--
-- TOC entry 3948 (class 0 OID 0)
-- Dependencies: 228
-- Name: comenzi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.comenzi_id_seq OWNED BY public.comenzi.id;

-- endregion Comenzi

--
-- TOC entry 263 (class 1259 OID 89908)
-- Name: compensari; Type: TABLE; Schema: public; Owner: postgres
--

-- region Compensari

CREATE TABLE public.compensari (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp character varying(8) DEFAULT ''::character varying NOT NULL,
    codp0 character varying(13) DEFAULT ''::character varying NOT NULL,
    um0 character varying(3) DEFAULT ''::character varying NOT NULL,
    cant0 numeric(13,4) DEFAULT 0 NOT NULL,
    pc0 numeric(20,8) DEFAULT 0 NOT NULL,
    pv0 numeric(20,8) DEFAULT 0 NOT NULL,
    contp2 character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp2 character varying(8) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    denp character varying(8) DEFAULT ''::character varying NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

-- Comments on compensari table
COMMENT ON TABLE public.compensari IS 'Echivalent compens.dbf <= Document Compensari <= Fisier Unic';
COMMENT ON COLUMN public.compensari.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.compensari.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.compensari.retrieved IS 'The column used to synch data downward';

--

ALTER TABLE public.compensari OWNER TO postgres;

--
-- TOC entry 262 (class 1259 OID 89906)
-- Name: compensari_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.compensari_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.compensari_id_seq OWNER TO postgres;

--
-- TOC entry 3949 (class 0 OID 0)
-- Dependencies: 262
-- Name: compensari_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.compensari_id_seq OWNED BY public.compensari.id;

-- endregion Compensari

--
-- TOC entry 221 (class 1259 OID 89415)
-- Name: contracte; Type: TABLE; Schema: public; Owner: postgres
--

-- region Contracte
CREATE TABLE public.contracte (
    id integer NOT NULL,
    local_id integer NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    nrcontr numeric(9,0) DEFAULT 0 NOT NULL,
    datacontr date DEFAULT ('now'::text)::date NOT NULL,
    explic character varying(244) DEFAULT ''::character varying NOT NULL,
    obs character varying(244) DEFAULT ''::character varying NOT NULL,
    zilesc numeric(4,0) DEFAULT 0 NOT NULL,
    tipplata numeric(1,0) DEFAULT 0 NOT NULL,
    scont numeric(8,4) DEFAULT 0 NOT NULL,
    tipcontr character varying(10) DEFAULT ''::character varying NOT NULL,
    valoare numeric(15,4) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    datafin date DEFAULT ('now'::text)::date NOT NULL,
    soldini numeric(15,4) DEFAULT 0 NOT NULL,
    persoana character varying(20) DEFAULT ''::character varying NOT NULL,
    termenplata numeric(10,0) DEFAULT 0 NOT NULL,
    penalitati numeric(10,2) DEFAULT 0 NOT NULL,
    zilesc_otc numeric(4,0) DEFAULT 0 NOT NULL,
    zona character varying(20) DEFAULT ''::character varying NOT NULL,
    synched boolean default false NOT NULL,
    retrieved boolean default true NOT NULL
);

-- Comments on contracte table
COMMENT ON TABLE public.contracte IS 'Echivalent contr.dbf <= Document Contracte <= Fisier Unic';
COMMENT ON COLUMN public.contracte.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.contracte.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.contracte.retrieved IS 'The column used to synch data downward';
--

ALTER TABLE public.contracte OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 89413)
-- Name: contracte_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.contracte_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.contracte_id_seq OWNER TO postgres;

--
-- TOC entry 3950 (class 0 OID 0)
-- Dependencies: 220
-- Name: contracte_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.contracte_id_seq OWNED BY public.contracte.id;

-- endregion Contracte

--
-- TOC entry 213 (class 1259 OID 89293)
-- Name: conturi; Type: TABLE; Schema: public; Owner: postgres
--

--region Conturi

CREATE TABLE public.conturi (
    id integer NOT NULL,
    local_id integer NOT NULL,
    felc numeric(1,0) DEFAULT 0 NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    subo numeric(1,0) DEFAULT 0 NOT NULL,
    den character varying(30) DEFAULT ''::character varying NOT NULL,
    sid numeric(20,8) DEFAULT 0 NOT NULL,
    sic numeric(20,8) DEFAULT 0 NOT NULL,
    pd numeric(20,8) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    rd numeric(20,8) DEFAULT 0 NOT NULL,
    rc numeric(20,8) DEFAULT 0 NOT NULL,
    sfd numeric(20,8) DEFAULT 0 NOT NULL,
    sfc numeric(20,8) DEFAULT 0 NOT NULL,
    sidval numeric(20,8) DEFAULT 0 NOT NULL,
    sicval numeric(20,8) DEFAULT 0 NOT NULL,
    pdval numeric(20,8) DEFAULT 0 NOT NULL,
    pcval numeric(20,8) DEFAULT 0 NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,  
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

-- Comments on plan conturi table
COMMENT ON TABLE public.conturi IS 'Echivalent fcmmyyyy.dbf <= Nomenclator Plan Conturi <= Fisier Lunar';
COMMENT ON COLUMN public.conturi.local_id IS 'File id on the local nom file';
COMMENT ON COLUMN public.conturi.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.conturi.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.conturi.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.conturi.lunadoc IS 'Column is mandatory for the mmyyyy files';
--


ALTER TABLE public.conturi OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 89291)
-- Name: conturi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.conturi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.conturi_id_seq OWNER TO postgres;

--
-- TOC entry 3951 (class 0 OID 0)
-- Dependencies: 212
-- Name: conturi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.conturi_id_seq OWNED BY public.conturi.id;

-- endregion conturi

--
-- TOC entry 219 (class 1259 OID 89398)
-- Name: curs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.curs (
    id integer NOT NULL,
    local_id integer NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    cursof numeric(11,4) DEFAULT 0 NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

-- Comments on curs table
COMMENT ON TABLE public.curs IS 'Echivalent curs.dbf <= Nomenclator Cursuri Valutar <= Fisier Unic';
COMMENT ON COLUMN public.curs.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.curs.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.curs.retrieved IS 'The column used to synch data downward';
--

ALTER TABLE public.curs OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 89396)
-- Name: curs_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.curs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.curs_id_seq OWNER TO postgres;

--
-- TOC entry 3952 (class 0 OID 0)
-- Dependencies: 218
-- Name: curs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.curs_id_seq OWNED BY public.curs.id;

-- endregion Curs

--
-- TOC entry 199 (class 1259 OID 89015)
-- Name: datablock; Type: TABLE; Schema: public; Owner: postgres
--

-- region Datablock

CREATE TABLE public.datablock (
    id integer NOT NULL,
    bloc character varying DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.datablock OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 89013)
-- Name: datablock_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.datablock_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.datablock_id_seq OWNER TO postgres;

--
-- TOC entry 3953 (class 0 OID 0)
-- Dependencies: 198
-- Name: datablock_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.datablock_id_seq OWNED BY public.datablock.id;

-- endregion Datablock

--
-- TOC entry 271 (class 1259 OID 89995)
-- Name: dispozitii_livrare; Type: TABLE; Schema: public; Owner: postgres
--

-- region Dispozitii Livrare

CREATE TABLE public.dispozitii_livrare (
    id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    delegat character varying(30) DEFAULT ''::character varying NOT NULL,
    nrdeleg character varying(30) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(20) DEFAULT ''::character varying NOT NULL,
    bi character varying(20) DEFAULT ''::character varying NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(15,4) DEFAULT 0 NOT NULL,
    cantlivr numeric(15,4) DEFAULT 0 NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.dispozitii_livrare OWNER TO postgres;

--
-- TOC entry 270 (class 1259 OID 89993)
-- Name: dispozitii_livrare_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.dispozitii_livrare_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.dispozitii_livrare_id_seq OWNER TO postgres;

--
-- TOC entry 3954 (class 0 OID 0)
-- Dependencies: 270
-- Name: dispozitii_livrare_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.dispozitii_livrare_id_seq OWNED BY public.dispozitii_livrare.id;

-- endregion Dispozitii Livrare

--
-- TOC entry 197 (class 1259 OID 88937)
-- Name: facturi; Type: TABLE; Schema: public; Owner: postgres
--

-- region Facturi

CREATE TABLE public.facturi (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    agent character varying(10) DEFAULT ''::character varying NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    achit character varying(40) DEFAULT ''::character varying NOT NULL,
    delegat character varying(25) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(15) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(250) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    prad numeric(10,4) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    nrcontr numeric(10,0) DEFAULT 0 NOT NULL,
    jurntva character varying(3) DEFAULT ''::character varying NOT NULL,
    zona character varying(20) DEFAULT ''::character varying NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    codfiscal character varying(20) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    div character varying(100) DEFAULT ''::character varying NOT NULL,
    electro character varying(100) DEFAULT ''::character varying NOT NULL,
    pcvpv character varying(3) DEFAULT ''::character varying NOT NULL,
    tip394 character varying(10) DEFAULT ''::character varying NOT NULL,
    pnum numeric(15,2) DEFAULT 0 NOT NULL,
    pcard numeric(15,2) DEFAULT 0 NOT NULL,
    pbonm numeric(15,2) DEFAULT 0 NOT NULL,
    pvira numeric(15,2) DEFAULT 0 NOT NULL,
    cui character varying(20) DEFAULT 0 NOT NULL,
    nume character varying(30) DEFAULT 0 NOT NULL,
    judet character varying(2) DEFAULT ''::character varying NOT NULL,
    tara character varying(2) DEFAULT ''::character varying NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Facturi
COMMENT ON TABLE public.facturi IS 'Echivalent fammyyyy.dbf <= Document Facturi <= Fisier Lunar';
COMMENT ON COLUMN public.facturi.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.facturi.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.facturi.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.facturi.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.facturi.lunadoc IS 'Column is mandatory for the mmyyyy files';
--


ALTER TABLE public.facturi OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 88935)
-- Name: facturi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.facturi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.facturi_id_seq OWNER TO postgres;

--
-- TOC entry 3955 (class 0 OID 0)
-- Dependencies: 196
-- Name: facturi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.facturi_id_seq OWNED BY public.facturi.id;

-- endregion facturi

--
-- TOC entry 209 (class 1259 OID 89233)
-- Name: financiar; Type: TABLE; Schema: public; Owner: postgres
--

-- region Financiar

CREATE TABLE public.financiar (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    nrreg numeric(10,0) DEFAULT 0 NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp character varying(8) DEFAULT ''::character varying NOT NULL,
    contg character varying(8) DEFAULT ''::character varying NOT NULL,
    suma numeric(20,8) DEFAULT 0 NOT NULL,
    rd numeric(20,8) DEFAULT 0 NOT NULL,
    explic character varying(35) DEFAULT ''::character varying NOT NULL,
    tva numeric(2,0) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    jurntva character varying(3) DEFAULT ''::character varying NOT NULL,
    nrcontr numeric(10,0) DEFAULT 0 NOT NULL,
    nid numeric(10,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    casier character varying(10) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    tip394 character varying(10) DEFAULT ''::character varying NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Financiar
COMMENT ON TABLE public.financiar IS 'Echivalent fimmyyyy.dbf <= Document Financiar <= Fisier Lunar';
COMMENT ON COLUMN public.financiar.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.financiar.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.financiar.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.financiar.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.financiar.lunadoc IS 'Column is mandatory for the mmyyyy files';
--


ALTER TABLE public.financiar OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 89231)
-- Name: financiar_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.financiar_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.financiar_id_seq OWNER TO postgres;

--
-- TOC entry 3956 (class 0 OID 0)
-- Dependencies: 208
-- Name: financiar_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.financiar_id_seq OWNED BY public.financiar.id;

-- endregion Financiar

--
-- TOC entry 241 (class 1259 OID 89616)
-- Name: gestiuni; Type: TABLE; Schema: public; Owner: postgres
--

-- region Gestiuni

CREATE TABLE public.gestiuni (
    id integer NOT NULL,
    local_id integer NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    felg character varying(1) DEFAULT ''::character varying NOT NULL,
    deng character varying(50) DEFAULT ''::character varying NOT NULL,
    sgest character varying(4) DEFAULT ''::character varying NOT NULL,
    sector character varying(30) DEFAULT ''::character varying NOT NULL,
    sef character varying(50) DEFAULT ''::character varying NOT NULL,
    adresa character varying(100) DEFAULT ''::character varying NOT NULL,
    localitate character varying(50) DEFAULT ''::character varying NOT NULL,
    judet character varying(50) DEFAULT ''::character varying NOT NULL,
    tipg character varying(10) DEFAULT ''::character varying NOT NULL,
    lastfact numeric(10,0) DEFAULT 0 NOT NULL,
    seriefact numeric(10,0) DEFAULT 0 NOT NULL,
    lasttran numeric(10,0) DEFAULT 0 NOT NULL,
    lastnir numeric(10,0) DEFAULT 0 NOT NULL,
    lastbco numeric(10,0) DEFAULT 0 NOT NULL,
    lastprv numeric(10,0) DEFAULT 0 NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT false NOT NULL
);

--Comments for table Gestiuni
COMMENT ON TABLE public.gestiuni IS 'Echivalent gest.dbf <= Nomenclator Gestiuni <= Fisier Unic';
COMMENT ON COLUMN public.gestiuni.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.gestiuni.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.gestiuni.retrieved IS 'The column used to synch data downward';
--


ALTER TABLE public.gestiuni OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 89614)
-- Name: gestiuni_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.gestiuni_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.gestiuni_id_seq OWNER TO postgres;

--
-- TOC entry 3957 (class 0 OID 0)
-- Dependencies: 240
-- Name: gestiuni_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.gestiuni_id_seq OWNED BY public.gestiuni.id;

-- endregion Gestiuni

--
-- TOC entry 225 (class 1259 OID 89467)
-- Name: grupe; Type: TABLE; Schema: public; Owner: postgres
--

-- region Grupe

CREATE TABLE public.grupe (
    id integer NOT NULL,
    grupa character varying(3) DEFAULT ''::character varying NOT NULL,
    dengr character varying(30) DEFAULT ''::character varying NOT NULL,
    grupas character varying(3) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.grupe OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 89465)
-- Name: grupe_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.grupe_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.grupe_id_seq OWNER TO postgres;

--
-- TOC entry 3958 (class 0 OID 0)
-- Dependencies: 224
-- Name: grupe_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.grupe_id_seq OWNED BY public.grupe.id;

-- endregion grupe

--
-- TOC entry 227 (class 1259 OID 89478)
-- Name: incadrare; Type: TABLE; Schema: public; Owner: postgres
--

-- region Incadrare

CREATE TABLE public.incadrare (
    id integer NOT NULL,
    incadrare character varying(11) DEFAULT ''::character varying NOT NULL,
    denumire character varying(50) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.incadrare OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 89476)
-- Name: incadrare_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.incadrare_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.incadrare_id_seq OWNER TO postgres;

--
-- TOC entry 3959 (class 0 OID 0)
-- Dependencies: 226
-- Name: incadrare_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.incadrare_id_seq OWNED BY public.incadrare.id;

-- endregion Incadrare

--
-- TOC entry 237 (class 1259 OID 89582)
-- Name: inventar; Type: TABLE; Schema: public; Owner: postgres
--

-- region Inventar

CREATE TABLE public.inventar (
    id integer NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.inventar OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 89580)
-- Name: inventar_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.inventar_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.inventar_id_seq OWNER TO postgres;

--
-- TOC entry 3960 (class 0 OID 0)
-- Dependencies: 236
-- Name: inventar_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.inventar_id_seq OWNED BY public.inventar.id;

-- endregion Inventar

--
-- TOC entry 259 (class 1259 OID 89868)
-- Name: lastcod; Type: TABLE; Schema: public; Owner: postgres
--

-- region Lastcod
-- #warning TBD:

CREATE TABLE public.lastcod (
    id integer NOT NULL,
    transfer numeric(10,0) DEFAULT 0 NOT NULL,
    factura numeric(10,0) DEFAULT 0 NOT NULL,
    chitanta numeric(10,0) DEFAULT 0 NOT NULL,
    produs numeric(13,0) DEFAULT 0 NOT NULL,
    ftp numeric(10,0) DEFAULT 0 NOT NULL,
    comanda numeric(10,0) DEFAULT 0 NOT NULL,
    service numeric(10,0) DEFAULT 0 NOT NULL,
    partener numeric(10,0) DEFAULT 0 NOT NULL,
    centraliz numeric(10,0) DEFAULT 0 NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    bonfisc numeric(10,0) DEFAULT 0 NOT NULL,
    fi_nid numeric(10,0) DEFAULT 0 NOT NULL,
    facttemp numeric(10,0) DEFAULT 0 NOT NULL,
    facturaex numeric(10,0) DEFAULT 0 NOT NULL
);


ALTER TABLE public.lastcod OWNER TO postgres;

--
-- TOC entry 258 (class 1259 OID 89866)
-- Name: lastcod_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.lastcod_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.lastcod_id_seq OWNER TO postgres;

--
-- TOC entry 3961 (class 0 OID 0)
-- Dependencies: 258
-- Name: lastcod_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.lastcod_id_seq OWNED BY public.lastcod.id;

-- endregion lastcod

--
-- TOC entry 269 (class 1259 OID 89970)
-- Name: limita_consum; Type: TABLE; Schema: public; Owner: postgres
--

-- region Limita Consum

CREATE TABLE public.limita_consum (
    id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    codlucrare character varying(13) DEFAULT ''::character varying NOT NULL,
    denlucrare character varying(50) DEFAULT ''::character varying NOT NULL,
    norma numeric(16,6) DEFAULT 0 NOT NULL,
    planificat numeric(15,4) DEFAULT 0 NOT NULL,
    realizat numeric(15,4) DEFAULT 0 NOT NULL,
    norma2 numeric(15,6) DEFAULT 0 NOT NULL,
    planificat2 numeric(15,4) DEFAULT 0 NOT NULL,
    realizat2 numeric(15,4) DEFAULT 0 NOT NULL,
    pc numeric(15,4) DEFAULT 0 NOT NULL,
    pv numeric(15,4) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.limita_consum OWNER TO postgres;

--
-- TOC entry 268 (class 1259 OID 89968)
-- Name: limita_consum_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.limita_consum_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.limita_consum_id_seq OWNER TO postgres;

--
-- TOC entry 3962 (class 0 OID 0)
-- Dependencies: 268
-- Name: limita_consum_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.limita_consum_id_seq OWNED BY public.limita_consum.id;

-- endregion Limita Consum

--
-- TOC entry 265 (class 1259 OID 89936)
-- Name: loturi; Type: TABLE; Schema: public; Owner: postgres
--

-- region Loturi

CREATE TABLE public.loturi (
    id integer NOT NULL,
    nid character varying(10) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    lot character varying(20) DEFAULT ''::character varying NOT NULL,
    dataexp date DEFAULT ('now'::text)::date NOT NULL,
    cantiesita numeric(15,4) DEFAULT 0 NOT NULL,
    cant numeric(15,4) DEFAULT 0 NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fisdoc character varying(10) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    tora character varying(20) DEFAULT ''::character varying NOT NULL,
    obs character varying(50) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.loturi OWNER TO postgres;

--
-- TOC entry 264 (class 1259 OID 89934)
-- Name: loturi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.loturi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.loturi_id_seq OWNER TO postgres;

--
-- TOC entry 3963 (class 0 OID 0)
-- Dependencies: 264
-- Name: loturi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.loturi_id_seq OWNED BY public.loturi.id;

-- endregion Loturi

--
-- TOC entry 251 (class 1259 OID 89759)
-- Name: masini; Type: TABLE; Schema: public; Owner: postgres
--

-- region Masini

CREATE TABLE public.masini (
    id integer NOT NULL,
    maina character varying(10) DEFAULT ''::character varying NOT NULL,
    sofer character varying(30) DEFAULT ''::character varying NOT NULL,
    cnp character varying(15) DEFAULT ''::character varying NOT NULL,
    bi character varying(15) DEFAULT ''::character varying NOT NULL,
    tonaj numeric(10,4) DEFAULT 0 NOT NULL,
    autoriz character varying(30) DEFAULT ''::character varying NOT NULL,
    codmasina numeric(10,0) DEFAULT 0 NOT NULL,
    numar character varying(15) DEFAULT ''::character varying NOT NULL,
    categorie character varying(30) DEFAULT ''::character varying NOT NULL,
    marca character varying(30) DEFAULT ''::character varying NOT NULL,
    tip character varying(30) DEFAULT ''::character varying NOT NULL,
    model character varying(30) DEFAULT ''::character varying NOT NULL,
    sasiu character varying(17) DEFAULT ''::character varying NOT NULL,
    culoare character varying(20) DEFAULT ''::character varying NOT NULL,
    combustibil character varying(20) DEFAULT ''::character varying NOT NULL,
    inel_old boolean DEFAULT false NOT NULL,
    pincard character varying(10) DEFAULT ''::character varying NOT NULL,
    consum numeric(6,2) DEFAULT 0 NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    codsofer character varying(4) DEFAULT ''::character varying NOT NULL,
    proprietar character varying(30) DEFAULT ''::character varying NOT NULL,
    certificat character varying(15) DEFAULT ''::character varying NOT NULL,
    eliberatde character varying(30) DEFAULT ''::character varying NOT NULL,
    dataexpcer date DEFAULT ('now'::text)::date NOT NULL,
    carteident character varying(10) DEFAULT ''::character varying NOT NULL,
    datainmatr date DEFAULT ('now'::text)::date NOT NULL,
    anfabricat numeric(4,0) DEFAULT 0 NOT NULL,
    dotari character varying(240) DEFAULT ''::character varying NOT NULL,
    lunigarant numeric(7,0) DEFAULT 0 NOT NULL,
    kmgarant numeric(7,0) DEFAULT 0 NOT NULL,
    localitate character varying(20) DEFAULT ''::character varying NOT NULL,
    flota character varying(10) DEFAULT ''::character varying NOT NULL,
    observatii character varying(200) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.masini OWNER TO postgres;

--
-- TOC entry 250 (class 1259 OID 89757)
-- Name: masini_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.masini_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.masini_id_seq OWNER TO postgres;

--
-- TOC entry 3964 (class 0 OID 0)
-- Dependencies: 250
-- Name: masini_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.masini_id_seq OWNED BY public.masini.id;

-- endregion Masini

--
-- TOC entry 203 (class 1259 OID 89115)
-- Name: niruri; Type: TABLE; Schema: public; Owner: postgres
--

-- region Niruri

CREATE TABLE public.niruri (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    nrfact numeric(10,0) DEFAULT 0 NOT NULL,
    dataf date DEFAULT ('now'::text)::date NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    nrcom numeric(10,0) DEFAULT 0 NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    tvad numeric(2,0) DEFAULT 0 NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cantdoc numeric(13,4) DEFAULT 0 NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    procad numeric(6,2) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    pcval numeric(2,0) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    nrcontr numeric(10,0) DEFAULT 0 NOT NULL,
    jurntva character varying(3) DEFAULT ''::character varying NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    tip394 character varying(10) DEFAULT ''::character varying NOT NULL,
    cui character varying(20) DEFAULT ''::character varying NOT NULL,
    nume character varying(30) DEFAULT ''::character varying NOT NULL,
    judet character varying(2) DEFAULT ''::character varying NOT NULL,
    tara character varying(2) DEFAULT ''::character varying NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Niruri
COMMENT ON TABLE public.niruri IS 'Echivalent nimmyyyy.dbf <= Document Niruri <= Fisier Lunar';
COMMENT ON COLUMN public.niruri.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.niruri.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.niruri.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.niruri.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.niruri.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.niruri OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 89113)
-- Name: niruri_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.niruri_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.niruri_id_seq OWNER TO postgres;

--
-- TOC entry 3965 (class 0 OID 0)
-- Dependencies: 202
-- Name: niruri_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.niruri_id_seq OWNED BY public.niruri.id;

-- endregion Niruri

--
-- TOC entry 239 (class 1259 OID 89599)
-- Name: operatori; Type: TABLE; Schema: public; Owner: postgres
--

-- region Operatori

CREATE TABLE public.operatori (
    id integer NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    nume character varying(25) DEFAULT ''::character varying NOT NULL,
    bi character varying(20) DEFAULT ''::character varying NOT NULL,
    bidata date DEFAULT ('now'::text)::date NOT NULL,
    cnp character varying(13) DEFAULT ''::character varying NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    image character varying(50) DEFAULT ''::character varying NOT NULL,
    parola character varying(15) DEFAULT ''::character varying NOT NULL,
    manager_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.operatori OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 89597)
-- Name: operatori_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.operatori_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.operatori_id_seq OWNER TO postgres;

--
-- TOC entry 3966 (class 0 OID 0)
-- Dependencies: 238
-- Name: operatori_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.operatori_id_seq OWNED BY public.operatori.id;

-- endregion Operatori

--
-- TOC entry 235 (class 1259 OID 89552)
-- Name: ordin_plata; Type: TABLE; Schema: public; Owner: postgres
--

-- region Ordin Plata

CREATE TABLE public.ordin_plata (
    id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrreg numeric(10,0) DEFAULT 0 NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    suma numeric(20,8) DEFAULT 0 NOT NULL,
    fact character varying DEFAULT 0 NOT NULL,
    tipp1 character varying(1) DEFAULT 0 NOT NULL,
    explic character varying(70) DEFAULT 0 NOT NULL,
    codb character varying(9) DEFAULT 0 NOT NULL,
    numeb character varying(35) DEFAULT 0 NOT NULL,
    fbbanc character varying(30) DEFAULT 0 NOT NULL,
    fbcont character varying(30) DEFAULT 0 NOT NULL,
    cod1 character varying(1) DEFAULT 0 NOT NULL,
    data1 date DEFAULT ('now'::text)::date NOT NULL,
    cod2 character varying(1) DEFAULT 0 NOT NULL,
    data2 date DEFAULT ('now'::text)::date NOT NULL,
    z_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.ordin_plata OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 89550)
-- Name: ordin_plata_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ordin_plata_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.ordin_plata_id_seq OWNER TO postgres;

--
-- TOC entry 3967 (class 0 OID 0)
-- Dependencies: 234
-- Name: ordin_plata_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ordin_plata_id_seq OWNED BY public.ordin_plata.id;

-- endregion Ordin Plata

--
-- TOC entry 217 (class 1259 OID 89361)
-- Name: parteneri; Type: TABLE; Schema: public; Owner: postgres
--

-- region Parteneri

CREATE TABLE public.parteneri (
    id integer NOT NULL,
    local_id numeric NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    codf character varying(15) DEFAULT ''::character varying NOT NULL,
    denfb character varying(200) DEFAULT ''::character varying NOT NULL,
    codret character varying(4) DEFAULT ''::character varying NOT NULL,
    nrregcom character varying(20) DEFAULT ''::character varying NOT NULL,
    bn character varying(35) DEFAULT ''::character varying NOT NULL,
    cn character varying(30) DEFAULT ''::character varying NOT NULL,
    cncec character varying(50) DEFAULT ''::character varying NOT NULL,
    agent2 character varying(10) DEFAULT ''::character varying NOT NULL,
    numeadm character varying(20) DEFAULT ''::character varying NOT NULL,
    fel character varying(20) DEFAULT ''::character varying NOT NULL,
    autoriz character varying(50) DEFAULT ''::character varying NOT NULL,
    adresa character varying(100) DEFAULT ''::character varying NOT NULL,
    telefon character varying(100) DEFAULT ''::character varying NOT NULL,
    loc character varying(30) DEFAULT ''::character varying NOT NULL,
    zona character varying(30) DEFAULT ''::character varying NOT NULL,
    jud character varying(30) DEFAULT ''::character varying NOT NULL,
    tara character varying(25) DEFAULT ''::character varying NOT NULL,
    tiptva numeric(6,0) DEFAULT 0 NOT NULL,
    email character varying(50) DEFAULT ''::character varying NOT NULL,
    text1 character varying(100) DEFAULT ''::character varying NOT NULL,
    text2 character varying(100) DEFAULT ''::character varying NOT NULL,
    text3 character varying(100) DEFAULT ''::character varying NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Parteneri
COMMENT ON TABLE public.parteneri IS 'Echivalent fb.dbf <= Nomenclator Parteneri <= Fisier Lunar';
COMMENT ON COLUMN public.parteneri.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.parteneri.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.parteneri.retrieved IS 'The column used to synch data downward';
--

ALTER TABLE public.parteneri OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 89359)
-- Name: parteneri_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.parteneri_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.parteneri_id_seq OWNER TO postgres;

--
-- TOC entry 3968 (class 0 OID 0)
-- Dependencies: 216
-- Name: parteneri_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.parteneri_id_seq OWNED BY public.parteneri.id;

-- endregion Parteneri

--
-- TOC entry 261 (class 1259 OID 89891)
-- Name: preturi_multiple; Type: TABLE; Schema: public; Owner: postgres
--

-- region Preturi Multiple 

CREATE TABLE public.preturi_multiple (
    id integer NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    pv numeric(15,2) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    tora character varying(10) DEFAULT ''::character varying NOT NULL,
    obs character varying(50) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.preturi_multiple OWNER TO postgres;

--
-- TOC entry 260 (class 1259 OID 89889)
-- Name: preturi_multiple_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.preturi_multiple_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.preturi_multiple_id_seq OWNER TO postgres;

--
-- TOC entry 3969 (class 0 OID 0)
-- Dependencies: 260
-- Name: preturi_multiple_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.preturi_multiple_id_seq OWNED BY public.preturi_multiple.id;

-- endregion Preturi Multiple

--
-- TOC entry 207 (class 1259 OID 89194)
-- Name: procese_verbale; Type: TABLE; Schema: public; Owner: postgres
--

-- region Procese Verbale

CREATE TABLE public.procese_verbale (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    felp character varying(2) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    comanda numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    delegat character varying(25) DEFAULT ''::character varying NOT NULL,
    delegatbi character varying(20) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(15) DEFAULT ''::character varying NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp character varying(8) DEFAULT ''::character varying NOT NULL,
    contg character varying(8) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL

);

--Comments for table Procese Verbale
COMMENT ON TABLE public.procese_verbale IS 'Echivalent prmmyyyy.dbf <= Document Procese Verbale <= Fisier Lunar';
COMMENT ON COLUMN public.procese_verbale.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.procese_verbale.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.procese_verbale.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.procese_verbale.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.procese_verbale.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.procese_verbale OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 89192)
-- Name: procese_verbale_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.procese_verbale_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.procese_verbale_id_seq OWNER TO postgres;

--
-- TOC entry 3970 (class 0 OID 0)
-- Dependencies: 206
-- Name: procese_verbale_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.procese_verbale_id_seq OWNED BY public.procese_verbale.id;

-- endregion Procese Verbale

--
-- TOC entry 215 (class 1259 OID 89317)
-- Name: produse; Type: TABLE; Schema: public; Owner: postgres
--

-- region Produse

CREATE TABLE public.produse (
    id integer NOT NULL,
    local_id integer NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denm character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    tva numeric(2,0) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    grupa character varying(3) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    producator character varying(4) DEFAULT ''::character varying NOT NULL,
    pcval numeric(20,8) DEFAULT 0 NOT NULL,
    pvval numeric(20,8) DEFAULT 0 NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    um2 character varying(6) DEFAULT ''::character varying NOT NULL,
    um1um2 numeric(10,4) DEFAULT 0 NOT NULL,
    um3 character varying(6) DEFAULT ''::character varying NOT NULL,
    um2um3 numeric(10,4) DEFAULT 0 NOT NULL,
    incadrare character varying(11) DEFAULT ''::character varying NOT NULL,
    ct numeric(11,4) DEFAULT 0 NOT NULL,
    net numeric(11,4) DEFAULT 0 NOT NULL,
    reteta_old boolean DEFAULT false NOT NULL,
    formula character varying(25) DEFAULT ''::character varying NOT NULL,
    dataexp date DEFAULT ('now'::text)::date NOT NULL,
    lot character varying(100) DEFAULT ''::character varying NOT NULL,
    codtimbru character varying(13) DEFAULT ''::character varying NOT NULL,
    divizie character varying(20) DEFAULT ''::character varying NOT NULL,
    psmentor numeric(10,0) DEFAULT 0 NOT NULL,
    codext character varying(20) DEFAULT ''::character varying NOT NULL,
    denext character varying(100) DEFAULT ''::character varying NOT NULL,
    cod394 character varying(20) DEFAULT ''::character varying NOT NULL,
    pcrol numeric(20,8) DEFAULT 0 NOT NULL,
    pvrol numeric(20,8) DEFAULT 0 NOT NULL,
    pcvrol numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Produse
COMMENT ON TABLE public.produse IS 'Echivalent fp.dbf <= Nomenclator Produse <= Fisier Unic';
COMMENT ON COLUMN public.produse.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.produse.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.produse.retrieved IS 'The column used to synch data downward';
--

ALTER TABLE public.produse OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 89315)
-- Name: produse_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.produse_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.produse_id_seq OWNER TO postgres;

--
-- TOC entry 3971 (class 0 OID 0)
-- Dependencies: 214
-- Name: produse_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.produse_id_seq OWNED BY public.produse.id;

-- endregion Produse

--
-- TOC entry 231 (class 1259 OID 89509)
-- Name: proforma; Type: TABLE; Schema: public; Owner: postgres
--

-- region Proforma

CREATE TABLE public.proforma (
    id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    pcvpv character varying(3) DEFAULT ''::character varying NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    nrfact numeric(10,0) DEFAULT 0 NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.proforma OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 89507)
-- Name: proforma_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.proforma_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.proforma_id_seq OWNER TO postgres;

--
-- TOC entry 3972 (class 0 OID 0)
-- Dependencies: 230
-- Name: proforma_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.proforma_id_seq OWNED BY public.proforma.id;

-- endregion Proforma

--
-- TOC entry 273 (class 1259 OID 90019)
-- Name: retete; Type: TABLE; Schema: public; Owner: postgres
--

-- region Retete

CREATE TABLE public.retete (
    id integer NOT NULL,
    codr character varying(13) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    cant numeric(15,4) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    proc_pcv numeric(20,8) DEFAULT 0 NOT NULL
);


ALTER TABLE public.retete OWNER TO postgres;

--
-- TOC entry 272 (class 1259 OID 90017)
-- Name: retete_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.retete_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.retete_id_seq OWNER TO postgres;

--
-- TOC entry 3973 (class 0 OID 0)
-- Dependencies: 272
-- Name: retete_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.retete_id_seq OWNED BY public.retete.id;

-- endregion Retete

--
-- TOC entry 247 (class 1259 OID 89711)
-- Name: scadentar; Type: TABLE; Schema: public; Owner: postgres
--

-- region Scadentar

CREATE TABLE public.scadentar (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    pacc character varying(3) DEFAULT ''::character varying NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    feld character varying(3) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    nrreg numeric(10,0) DEFAULT 0 NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp character varying(8) DEFAULT ''::character varying NOT NULL,
    rd numeric(20,8) DEFAULT 0 NOT NULL,
    rc numeric(20,8) DEFAULT 0 NOT NULL,
    agent character varying(8) DEFAULT ''::character varying NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    datac date DEFAULT ('now'::text)::date NOT NULL,
    codval character varying(8) DEFAULT ''::character varying NOT NULL,
    rdval numeric(20,8) DEFAULT 0 NOT NULL,
    rcval numeric(20,8) DEFAULT 0 NOT NULL,
    suma numeric(20,8) DEFAULT 0 NOT NULL,
    tvad numeric(2,0) DEFAULT 0 NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    cant numeric(11,3) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    pcv numeric(20,8) DEFAULT 0 NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(10) DEFAULT ''::character varying NOT NULL,
    nrgest numeric(5,0) DEFAULT 0 NOT NULL,
    rol_old boolean DEFAULT false NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Scadentar
COMMENT ON TABLE public.scadentar IS 'Echivalent scmmyyyy.dbf <= Document Scadentar <= Fisier Lunar';
COMMENT ON COLUMN public.scadentar.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.scadentar.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.scadentar.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.scadentar.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.scadentar.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.scadentar OWNER TO postgres;

--
-- TOC entry 246 (class 1259 OID 89709)
-- Name: scadentar_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.scadentar_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.scadentar_id_seq OWNER TO postgres;

--
-- TOC entry 3975 (class 0 OID 0)
-- Dependencies: 246
-- Name: scadentar_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.scadentar_id_seq OWNED BY public.scadentar.id;

-- endregion Scadentar

--
-- TOC entry 245 (class 1259 OID 89689)
-- Name: scadentar_clienti; Type: TABLE; Schema: public; Owner: postgres
--

-- region Scadentar Clienti

CREATE TABLE public.scadentar_clienti (
    id integer NOT NULL,
    contp character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    rd numeric(15,4) DEFAULT 0 NOT NULL,
    rc numeric(15,8) DEFAULT 0 NOT NULL,
    nrreg numeric(10,0) DEFAULT 0 NOT NULL,
    datac date DEFAULT ('now'::text)::date NOT NULL,
    nrregc character varying DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.scadentar_clienti OWNER TO postgres;

--
-- TOC entry 244 (class 1259 OID 89687)
-- Name: scadentar_clienti_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.scadentar_clienti_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.scadentar_clienti_id_seq OWNER TO postgres;

--
-- TOC entry 3974 (class 0 OID 0)
-- Dependencies: 244
-- Name: scadentar_clienti_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.scadentar_clienti_id_seq OWNED BY public.scadentar_clienti.id;

-- endregion Scadentar Clienti

--
-- TOC entry 255 (class 1259 OID 89818)
-- Name: service; Type: TABLE; Schema: public; Owner: postgres
--

-- region Service

CREATE TABLE public.service (
    id integer NOT NULL,
    nid numeric(10,0) DEFAULT 0 NOT NULL,
    masina numeric(6,0) DEFAULT 0 NOT NULL,
    tip_service numeric(6,0) DEFAULT 0 NOT NULL,
    sofer character varying(4) DEFAULT ''::character varying NOT NULL,
    descriere character varying(100) DEFAULT ''::character varying NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    km numeric(7,0) DEFAULT 0 NOT NULL,
    dataalerta date DEFAULT ('now'::text)::date NOT NULL,
    kmalerta numeric(7,0) DEFAULT 0 NOT NULL,
    cant numeric(10,3) DEFAULT 0 NOT NULL,
    pret numeric(15,2) DEFAULT 0 NOT NULL,
    alerta_old boolean DEFAULT false NOT NULL,
    rezolvat_old boolean DEFAULT false NOT NULL
);


ALTER TABLE public.service OWNER TO postgres;

--
-- TOC entry 254 (class 1259 OID 89816)
-- Name: service_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.service_id_seq OWNER TO postgres;

--
-- TOC entry 3976 (class 0 OID 0)
-- Dependencies: 254
-- Name: service_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.service_id_seq OWNED BY public.service.id;

-- endregion Service

--
-- TOC entry 211 (class 1259 OID 89271)
-- Name: stoc; Type: TABLE; Schema: public; Owner: postgres
--

-- region Stoc 

CREATE TABLE public.stoc (
    id integer NOT NULL,
    local_id integer NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    valc numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean DEFAULT true NOT NULL
);

--Comments for table Stoc
COMMENT ON TABLE public.stoc IS 'Echivalent stmmyyyy.dbf <= Document Stoc <= Fisier Lunar';
COMMENT ON COLUMN public.stoc.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.stoc.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.stoc.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.stoc.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.stoc.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.stoc OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 89269)
-- Name: stoc_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.stoc_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.stoc_id_seq OWNER TO postgres;

--
-- TOC entry 3977 (class 0 OID 0)
-- Dependencies: 210
-- Name: stoc_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.stoc_id_seq OWNED BY public.stoc.id;

-- endregion Stoc

--
-- TOC entry 233 (class 1259 OID 89536)
-- Name: stoc_minim_maxim; Type: TABLE; Schema: public; Owner: postgres
--

-- region Stoc Minim Maxim

CREATE TABLE public.stoc_minim_maxim (
    id integer NOT NULL,
    gest character varying(5) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    c_min numeric(13,3) DEFAULT 0 NOT NULL,
    c_max numeric(13,3) DEFAULT 0 NOT NULL
);


ALTER TABLE public.stoc_minim_maxim OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 89534)
-- Name: stoc_minim_maxim_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.stoc_minim_maxim_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.stoc_minim_maxim_id_seq OWNER TO postgres;

--
-- TOC entry 3978 (class 0 OID 0)
-- Dependencies: 232
-- Name: stoc_minim_maxim_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.stoc_minim_maxim_id_seq OWNED BY public.stoc_minim_maxim.id;

-- endregion Stoc Minim Maxim

--
-- TOC entry 275 (class 1259 OID 90032)
-- Name: stoc_real; Type: TABLE; Schema: public; Owner: postgres
--

-- region Stoc Real

CREATE TABLE public.stoc_real (
    id integer NOT NULL,
    gest character varying(3) DEFAULT ''::character varying NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    st numeric(15,4) DEFAULT 0 NOT NULL,
    fa numeric(15,4) DEFAULT 0 NOT NULL,
    ni numeric(15,4) DEFAULT 0 NOT NULL,
    tri numeric(15,4) DEFAULT 0 NOT NULL,
    tre numeric(15,4) DEFAULT 0 NOT NULL,
    bc numeric(15,4) DEFAULT 0 NOT NULL,
    pri numeric(15,4) DEFAULT 0 NOT NULL,
    pre numeric(15,4) DEFAULT 0 NOT NULL,
    fazi numeric(15,4) DEFAULT 0 NOT NULL,
    com numeric(15,4) DEFAULT 0 NOT NULL,
    udata date DEFAULT ('now'::text)::date NOT NULL,
    utime character varying(13) DEFAULT ''::character varying NOT NULL,
    ucalc character varying(50) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.stoc_real OWNER TO postgres;

--
-- TOC entry 274 (class 1259 OID 90030)
-- Name: stoc_real_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.stoc_real_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.stoc_real_id_seq OWNER TO postgres;

--
-- TOC entry 3979 (class 0 OID 0)
-- Dependencies: 274
-- Name: stoc_real_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.stoc_real_id_seq OWNED BY public.stoc_real.id;

-- endregion Stoc Real

--
-- TOC entry 267 (class 1259 OID 89958)
-- Name: text_suplimentar; Type: TABLE; Schema: public; Owner: postgres
--

-- region Text Suplimentar

CREATE TABLE public.text_suplimentar (
    id integer NOT NULL,
    fisdoc character varying(10) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    text character varying(250) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.text_suplimentar OWNER TO postgres;

--
-- TOC entry 266 (class 1259 OID 89956)
-- Name: text_suplimentar_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.text_suplimentar_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.text_suplimentar_id_seq OWNER TO postgres;

--
-- TOC entry 3980 (class 0 OID 0)
-- Dependencies: 266
-- Name: text_suplimentar_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.text_suplimentar_id_seq OWNED BY public.text_suplimentar.id;

-- endregion Text Suplimentar

--
-- TOC entry 253 (class 1259 OID 89804)
-- Name: tip_service; Type: TABLE; Schema: public; Owner: postgres
--

-- region Tip Service

CREATE TABLE public.tip_service (
    id integer NOT NULL,
    cod character varying(4) DEFAULT ''::character varying NOT NULL,
    denumire character varying(50) DEFAULT ''::character varying NOT NULL,
    kmtermen numeric(7,0) DEFAULT 0 NOT NULL,
    lunitermen numeric(6,0) DEFAULT 0 NOT NULL,
    zilealerta numeric(6,0) DEFAULT 0 NOT NULL,
    kmalerta numeric(7,0) DEFAULT 0 NOT NULL
);


ALTER TABLE public.tip_service OWNER TO postgres;

--
-- TOC entry 252 (class 1259 OID 89802)
-- Name: tip_service_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tip_service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tip_service_id_seq OWNER TO postgres;

--
-- TOC entry 3981 (class 0 OID 0)
-- Dependencies: 252
-- Name: tip_service_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tip_service_id_seq OWNED BY public.tip_service.id;

-- endregion

--
-- TOC entry 201 (class 1259 OID 89078)
-- Name: transferuri; Type: TABLE; Schema: public; Owner: postgres
--

-- region Transferuri

CREATE TABLE public.transferuri (
    id integer NOT NULL,
    local_id integer NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    aviz numeric(10,0) DEFAULT 0 NOT NULL,
    agent character varying(10) DEFAULT ''::character varying NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    delegat character varying(25) DEFAULT ''::character varying NOT NULL,
    delegatbi character varying(20) DEFAULT ''::character varying NOT NULL,
    mijltr character varying(15) DEFAULT ''::character varying NOT NULL,
    tvac numeric(2,0) DEFAULT 0 NOT NULL,
    codp character varying(13) DEFAULT ''::character varying NOT NULL,
    denp character varying(50) DEFAULT ''::character varying NOT NULL,
    um character varying(3) DEFAULT ''::character varying NOT NULL,
    cant numeric(13,4) DEFAULT 0 NOT NULL,
    pc numeric(20,8) DEFAULT 0 NOT NULL,
    pv numeric(20,8) DEFAULT 0 NOT NULL,
    procad numeric(6,2) DEFAULT 0 NOT NULL,
    pv2 numeric(20,8) DEFAULT 0 NOT NULL,
    z_old boolean DEFAULT false NOT NULL,
    operator character varying(10) DEFAULT ''::character varying NOT NULL,
    tora character varying(25) DEFAULT ''::character varying NOT NULL,
    lot character varying(30) DEFAULT ''::character varying NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    dl character varying(15) DEFAULT ''::character varying NOT NULL,
    andoc numeric(4,0) DEFAULT 0 NOT NULL,
    lunadoc numeric(2,0) DEFAULT 0 NOT NULL,
    blocat_old boolean DEFAULT false NOT NULL,
    synched boolean DEFAULT false NOT NULL,
    retrieved boolean default true NOT NULL
);


--Comments for table Transferuri
COMMENT ON TABLE public.transferuri IS 'Echivalent trmmyyyy.dbf <= Document Transferuri <= Fisier Lunar';
COMMENT ON COLUMN public.transferuri.local_id IS 'File id on the local dbf file';
COMMENT ON COLUMN public.transferuri.synched IS 'The column used to synch data upward';
COMMENT ON COLUMN public.transferuri.retrieved IS 'The column used to synch data downward';
COMMENT ON COLUMN public.transferuri.andoc IS 'Column is mandatory for the mmyyyy files';
COMMENT ON COLUMN public.transferuri.lunadoc IS 'Column is mandatory for the mmyyyy files';
--

ALTER TABLE public.transferuri OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 89076)
-- Name: transferuri_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.transferuri_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transferuri_id_seq OWNER TO postgres;

--
-- TOC entry 3982 (class 0 OID 0)
-- Dependencies: 200
-- Name: transferuri_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.transferuri_id_seq OWNED BY public.transferuri.id;


--
-- TOC entry 277 (class 1259 OID 90056)
-- Name: tranzactii; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tranzactii (
    id integer NOT NULL,
    nrnota numeric(3,0) DEFAULT 0 NOT NULL,
    contp character varying(8) DEFAULT ''::character varying NOT NULL,
    coresp character varying(8) DEFAULT ''::character varying NOT NULL,
    nrreg numeric(10,0) DEFAULT 0 NOT NULL,
    nrdoc numeric(10,0) DEFAULT 0 NOT NULL,
    data date DEFAULT ('now'::text)::date NOT NULL,
    fuben character varying(4) DEFAULT ''::character varying NOT NULL,
    explic character varying(35) DEFAULT ''::character varying NOT NULL,
    codart character varying(8) DEFAULT ''::character varying NOT NULL,
    program character varying(4) DEFAULT ''::character varying NOT NULL,
    codval character varying(5) DEFAULT ''::character varying NOT NULL,
    rd numeric(20,8) DEFAULT 0 NOT NULL,
    rc numeric(20,8) DEFAULT 0 NOT NULL,
    suma numeric(20,8) DEFAULT 0 NOT NULL,
    gest character varying(4) DEFAULT ''::character varying NOT NULL,
    tva numeric(2,0) DEFAULT 0 NOT NULL,
    datasc date DEFAULT ('now'::text)::date NOT NULL,
    sursap character varying(10) DEFAULT ''::character varying NOT NULL,
    pac character varying(3) DEFAULT ''::character varying NOT NULL,
    rol_old boolean DEFAULT false NOT NULL,
    "timestamp" character varying(50) DEFAULT ''::character varying NOT NULL,
    tip394 character varying(10) DEFAULT ''::character varying NOT NULL,
    cui character varying(20) DEFAULT ''::character varying NOT NULL,
    nume character varying(50) DEFAULT ''::character varying NOT NULL,
    judet character varying(2) DEFAULT ''::character varying NOT NULL,
    tara character varying(2) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.tranzactii OWNER TO postgres;

--
-- TOC entry 276 (class 1259 OID 90054)
-- Name: tranz_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tranz_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tranz_id_seq OWNER TO postgres;

--
-- TOC entry 3983 (class 0 OID 0)
-- Dependencies: 276
-- Name: tranz_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tranz_id_seq OWNED BY public.tranzactii.id;


--
-- TOC entry 249 (class 1259 OID 89749)
-- Name: zone; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.zone (
    id integer NOT NULL,
    zona character varying(4) DEFAULT ''::character varying NOT NULL,
    denumire character varying(30) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public.zone OWNER TO postgres;

--
-- TOC entry 248 (class 1259 OID 89747)
-- Name: zone_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.zone_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.zone_id_seq OWNER TO postgres;

--
-- TOC entry 3984 (class 0 OID 0)
-- Dependencies: 248
-- Name: zone_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.zone_id_seq OWNED BY public.zone.id;


--
-- TOC entry 3236 (class 2604 OID 89447)
-- Name: agenti id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.agenti ALTER COLUMN id SET DEFAULT nextval('public.agenti_id_seq'::regclass);


--
-- TOC entry 3482 (class 2604 OID 89842)
-- Name: avize id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.avize ALTER COLUMN id SET DEFAULT nextval('public.avize_id_seq'::regclass);


--
-- TOC entry 3034 (class 2604 OID 89166)
-- Name: bonuri_consum id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bonuri_consum ALTER COLUMN id SET DEFAULT nextval('public.bonuri_consum_id_seq'::regclass);


--
-- TOC entry 3352 (class 2604 OID 89643)
-- Name: comandax id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comandax ALTER COLUMN id SET DEFAULT nextval('public.comandax_id_seq'::regclass);


--
-- TOC entry 3259 (class 2604 OID 89491)
-- Name: comenzi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comenzi ALTER COLUMN id SET DEFAULT nextval('public.comenzi_id_seq'::regclass);


--
-- TOC entry 3529 (class 2604 OID 89911)
-- Name: compensari id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.compensari ALTER COLUMN id SET DEFAULT nextval('public.compensari_id_seq'::regclass);


--
-- TOC entry 3217 (class 2604 OID 89418)
-- Name: contracte id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracte ALTER COLUMN id SET DEFAULT nextval('public.contracte_id_seq'::regclass);


--
-- TOC entry 3134 (class 2604 OID 89296)
-- Name: conturi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conturi ALTER COLUMN id SET DEFAULT nextval('public.conturi_id_seq'::regclass);


--
-- TOC entry 3213 (class 2604 OID 89401)
-- Name: curs id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.curs ALTER COLUMN id SET DEFAULT nextval('public.curs_id_seq'::regclass);


--
-- TOC entry 2964 (class 2604 OID 89018)
-- Name: datablock id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.datablock ALTER COLUMN id SET DEFAULT nextval('public.datablock_id_seq'::regclass);


--
-- TOC entry 3588 (class 2604 OID 89998)
-- Name: dispozitii_livrare id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dispozitii_livrare ALTER COLUMN id SET DEFAULT nextval('public.dispozitii_livrare_id_seq'::regclass);


--
-- TOC entry 2917 (class 2604 OID 88940)
-- Name: facturi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturi ALTER COLUMN id SET DEFAULT nextval('public.facturi_id_seq'::regclass);


--
-- TOC entry 3089 (class 2604 OID 89236)
-- Name: financiar id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.financiar ALTER COLUMN id SET DEFAULT nextval('public.financiar_id_seq'::regclass);


--
-- TOC entry 3335 (class 2604 OID 89619)
-- Name: gestiuni id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.gestiuni ALTER COLUMN id SET DEFAULT nextval('public.gestiuni_id_seq'::regclass);


--
-- TOC entry 3252 (class 2604 OID 89470)
-- Name: grupe id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupe ALTER COLUMN id SET DEFAULT nextval('public.grupe_id_seq'::regclass);


--
-- TOC entry 3256 (class 2604 OID 89481)
-- Name: incadrare id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.incadrare ALTER COLUMN id SET DEFAULT nextval('public.incadrare_id_seq'::regclass);


--
-- TOC entry 3315 (class 2604 OID 89585)
-- Name: inventar id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventar ALTER COLUMN id SET DEFAULT nextval('public.inventar_id_seq'::regclass);


--
-- TOC entry 3503 (class 2604 OID 89871)
-- Name: lastcod id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lastcod ALTER COLUMN id SET DEFAULT nextval('public.lastcod_id_seq'::regclass);


--
-- TOC entry 3570 (class 2604 OID 89973)
-- Name: limita_consum id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.limita_consum ALTER COLUMN id SET DEFAULT nextval('public.limita_consum_id_seq'::regclass);


--
-- TOC entry 3550 (class 2604 OID 89939)
-- Name: loturi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loturi ALTER COLUMN id SET DEFAULT nextval('public.loturi_id_seq'::regclass);


--
-- TOC entry 3427 (class 2604 OID 89762)
-- Name: masini id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masini ALTER COLUMN id SET DEFAULT nextval('public.masini_id_seq'::regclass);


--
-- TOC entry 3008 (class 2604 OID 89118)
-- Name: niruri id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.niruri ALTER COLUMN id SET DEFAULT nextval('public.niruri_id_seq'::regclass);


--
-- TOC entry 3325 (class 2604 OID 89602)
-- Name: operatori id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operatori ALTER COLUMN id SET DEFAULT nextval('public.operatori_id_seq'::regclass);


--
-- TOC entry 3296 (class 2604 OID 89555)
-- Name: ordin_plata id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ordin_plata ALTER COLUMN id SET DEFAULT nextval('public.ordin_plata_id_seq'::regclass);


--
-- TOC entry 3188 (class 2604 OID 89364)
-- Name: parteneri id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.parteneri ALTER COLUMN id SET DEFAULT nextval('public.parteneri_id_seq'::regclass);


--
-- TOC entry 3519 (class 2604 OID 89894)
-- Name: preturi_multiple id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.preturi_multiple ALTER COLUMN id SET DEFAULT nextval('public.preturi_multiple_id_seq'::regclass);


--
-- TOC entry 3058 (class 2604 OID 89197)
-- Name: procese_verbale id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.procese_verbale ALTER COLUMN id SET DEFAULT nextval('public.procese_verbale_id_seq'::regclass);


--
-- TOC entry 3151 (class 2604 OID 89320)
-- Name: produse id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.produse ALTER COLUMN id SET DEFAULT nextval('public.produse_id_seq'::regclass);


--
-- TOC entry 3273 (class 2604 OID 89512)
-- Name: proforma id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.proforma ALTER COLUMN id SET DEFAULT nextval('public.proforma_id_seq'::regclass);


--
-- TOC entry 3604 (class 2604 OID 90022)
-- Name: retete id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.retete ALTER COLUMN id SET DEFAULT nextval('public.retete_id_seq'::regclass);


--
-- TOC entry 3393 (class 2604 OID 89714)
-- Name: scadentar id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.scadentar ALTER COLUMN id SET DEFAULT nextval('public.scadentar_id_seq'::regclass);


--
-- TOC entry 3382 (class 2604 OID 89692)
-- Name: scadentar_clienti id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.scadentar_clienti ALTER COLUMN id SET DEFAULT nextval('public.scadentar_clienti_id_seq'::regclass);


--
-- TOC entry 3468 (class 2604 OID 89821)
-- Name: service id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service ALTER COLUMN id SET DEFAULT nextval('public.service_id_seq'::regclass);


--
-- TOC entry 3119 (class 2604 OID 89274)
-- Name: stoc id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc ALTER COLUMN id SET DEFAULT nextval('public.stoc_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 89539)
-- Name: stoc_minim_maxim id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc_minim_maxim ALTER COLUMN id SET DEFAULT nextval('public.stoc_minim_maxim_id_seq'::regclass);


--
-- TOC entry 3610 (class 2604 OID 90035)
-- Name: stoc_real id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc_real ALTER COLUMN id SET DEFAULT nextval('public.stoc_real_id_seq'::regclass);


--
-- TOC entry 3565 (class 2604 OID 89961)
-- Name: text_suplimentar id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.text_suplimentar ALTER COLUMN id SET DEFAULT nextval('public.text_suplimentar_id_seq'::regclass);


--
-- TOC entry 3461 (class 2604 OID 89807)
-- Name: tip_service id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tip_service ALTER COLUMN id SET DEFAULT nextval('public.tip_service_id_seq'::regclass);


--
-- TOC entry 2966 (class 2604 OID 89081)
-- Name: transferuri id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transferuri ALTER COLUMN id SET DEFAULT nextval('public.transferuri_id_seq'::regclass);


--
-- TOC entry 3680 (class 2606 OID 89464)
-- Name: agenti agenti_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.agenti
    ADD CONSTRAINT agenti_pkey PRIMARY KEY (id);


--
-- TOC entry 3714 (class 2606 OID 89864)
-- Name: avize avize_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.avize
    ADD CONSTRAINT avize_pkey PRIMARY KEY (id);


--
-- TOC entry 3662 (class 2606 OID 89191)
-- Name: bonuri_consum bonuri_consum_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bonuri_consum
    ADD CONSTRAINT bonuri_consum_pkey PRIMARY KEY (id);


--
-- TOC entry 3700 (class 2606 OID 89674)
-- Name: comandax comandax_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comandax
    ADD CONSTRAINT comandax_pkey PRIMARY KEY (id);


--
-- TOC entry 3686 (class 2606 OID 89506)
-- Name: comenzi comenzi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comenzi
    ADD CONSTRAINT comenzi_pkey PRIMARY KEY (id);


--
-- TOC entry 3720 (class 2606 OID 89933)
-- Name: compensari compensari_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.compensari
    ADD CONSTRAINT compensari_pkey PRIMARY KEY (id);


--
-- TOC entry 3678 (class 2606 OID 89441)
-- Name: contracte contracte_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracte
    ADD CONSTRAINT contracte_pkey PRIMARY KEY (id);


--
-- TOC entry 3670 (class 2606 OID 89314)
-- Name: conturi conturi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conturi
    ADD CONSTRAINT conturi_pkey PRIMARY KEY (id);


--
-- TOC entry 3676 (class 2606 OID 89406)
-- Name: curs curs_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.curs
    ADD CONSTRAINT curs_pkey PRIMARY KEY (id);


--
-- TOC entry 3656 (class 2606 OID 89024)
-- Name: datablock datablock_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.datablock
    ADD CONSTRAINT datablock_pkey PRIMARY KEY (id);


--
-- TOC entry 3728 (class 2606 OID 90015)
-- Name: dispozitii_livrare dispozitii_livrare_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dispozitii_livrare
    ADD CONSTRAINT dispozitii_livrare_pkey PRIMARY KEY (id);


--
-- TOC entry 3654 (class 2606 OID 88991)
-- Name: facturi facturi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturi
    ADD CONSTRAINT facturi_pkey PRIMARY KEY (id);


--
-- TOC entry 3666 (class 2606 OID 89267)
-- Name: financiar financiar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.financiar
    ADD CONSTRAINT financiar_pkey PRIMARY KEY (id);


--
-- TOC entry 3698 (class 2606 OID 89637)
-- Name: gestiuni gestiuni_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.gestiuni
    ADD CONSTRAINT gestiuni_pkey PRIMARY KEY (id);


--
-- TOC entry 3682 (class 2606 OID 89475)
-- Name: grupe grupe_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupe
    ADD CONSTRAINT grupe_pkey PRIMARY KEY (id);


--
-- TOC entry 3684 (class 2606 OID 89485)
-- Name: incadrare incadrare_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.incadrare
    ADD CONSTRAINT incadrare_pkey PRIMARY KEY (id);


--
-- TOC entry 3694 (class 2606 OID 89596)
-- Name: inventar inventar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventar
    ADD CONSTRAINT inventar_pkey PRIMARY KEY (id);


--
-- TOC entry 3716 (class 2606 OID 89888)
-- Name: lastcod lastcod_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lastcod
    ADD CONSTRAINT lastcod_pkey PRIMARY KEY (id);


--
-- TOC entry 3726 (class 2606 OID 89992)
-- Name: limita_consum limita_consum_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.limita_consum
    ADD CONSTRAINT limita_consum_pkey PRIMARY KEY (id);


--
-- TOC entry 3722 (class 2606 OID 89955)
-- Name: loturi loturi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loturi
    ADD CONSTRAINT loturi_pkey PRIMARY KEY (id);


--
-- TOC entry 3708 (class 2606 OID 89800)
-- Name: masini masini_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masini
    ADD CONSTRAINT masini_pkey PRIMARY KEY (id);


--
-- TOC entry 3660 (class 2606 OID 89158)
-- Name: niruri niruri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.niruri
    ADD CONSTRAINT niruri_pkey PRIMARY KEY (id);


--
-- TOC entry 3696 (class 2606 OID 89613)
-- Name: operatori operatori_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operatori
    ADD CONSTRAINT operatori_pkey PRIMARY KEY (id);


--
-- TOC entry 3692 (class 2606 OID 89578)
-- Name: ordin_plata ordin_plata_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ordin_plata
    ADD CONSTRAINT ordin_plata_pkey PRIMARY KEY (id);


--
-- TOC entry 3674 (class 2606 OID 89393)
-- Name: parteneri parteneri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.parteneri
    ADD CONSTRAINT parteneri_pkey PRIMARY KEY (id);


--
-- TOC entry 3718 (class 2606 OID 89905)
-- Name: preturi_multiple preturi_multiple_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.preturi_multiple
    ADD CONSTRAINT preturi_multiple_pkey PRIMARY KEY (id);


--
-- TOC entry 3664 (class 2606 OID 89229)
-- Name: procese_verbale procese_verbale_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.procese_verbale
    ADD CONSTRAINT procese_verbale_pkey PRIMARY KEY (id);


--
-- TOC entry 3672 (class 2606 OID 89358)
-- Name: produse produse_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.produse
    ADD CONSTRAINT produse_pkey PRIMARY KEY (id);


--
-- TOC entry 3688 (class 2606 OID 89531)
-- Name: proforma proforma_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.proforma
    ADD CONSTRAINT proforma_pkey PRIMARY KEY (id);


--
-- TOC entry 3730 (class 2606 OID 90029)
-- Name: retete retete_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.retete
    ADD CONSTRAINT retete_pkey PRIMARY KEY (id);


--
-- TOC entry 3702 (class 2606 OID 89707)
-- Name: scadentar_clienti scadentar_clienti_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.scadentar_clienti
    ADD CONSTRAINT scadentar_clienti_pkey PRIMARY KEY (id);


--
-- TOC entry 3704 (class 2606 OID 89746)
-- Name: scadentar scadentar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.scadentar
    ADD CONSTRAINT scadentar_pkey PRIMARY KEY (id);


--
-- TOC entry 3712 (class 2606 OID 89836)
-- Name: service service_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_pkey PRIMARY KEY (id);


--
-- TOC entry 3690 (class 2606 OID 89545)
-- Name: stoc_minim_maxim stoc_minim_maxim_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc_minim_maxim
    ADD CONSTRAINT stoc_minim_maxim_pkey PRIMARY KEY (id);


--
-- TOC entry 3668 (class 2606 OID 89290)
-- Name: stoc stoc_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc
    ADD CONSTRAINT stoc_pkey PRIMARY KEY (id);


--
-- TOC entry 3732 (class 2606 OID 90052)
-- Name: stoc_real stoc_real_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stoc_real
    ADD CONSTRAINT stoc_real_pkey PRIMARY KEY (id);


--
-- TOC entry 3724 (class 2606 OID 89967)
-- Name: text_suplimentar text_suplimentar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.text_suplimentar
    ADD CONSTRAINT text_suplimentar_pkey PRIMARY KEY (id);


--
-- TOC entry 3710 (class 2606 OID 89815)
-- Name: tip_service tip_service_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tip_service
    ADD CONSTRAINT tip_service_pkey PRIMARY KEY (id);


--
-- TOC entry 3658 (class 2606 OID 89160)
-- Name: transferuri transferuri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transferuri
    ADD CONSTRAINT transferuri_pkey PRIMARY KEY (id);


--
-- TOC entry 3734 (class 2606 OID 90087)
-- Name: tranzactii tranz_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tranzactii
    ADD CONSTRAINT tranz_pkey PRIMARY KEY (id);


--
-- TOC entry 3706 (class 2606 OID 89756)
-- Name: zone zone_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.zone
    ADD CONSTRAINT zone_pkey PRIMARY KEY (id);


-- Completed on 2020-01-23 14:53:46