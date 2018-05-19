CREATE DATABASE interact WITH ENCODING='UTF8';

CREATE TABLE consumertype (
    id       serial PRIMARY KEY,
    name     varchar(100) NOT NULL,
	assembly varchar(500) NOT NULL,
    version  varchar(11) NOT NULL,
    disabled boolean NOT NULL DEFAULT false
);

CREATE TABLE workertype(
    id       serial PRIMARY KEY,
    name     varchar(100) NOT NULL,
	assembly varchar(500) NOT NULL,
    version  varchar(11) NOT NULL,
    disabled boolean NOT NULL DEFAULT false
);

CREATE TABLE cloudqueueconfig(
	id	    serial PRIMARY KEY,
	name    VARCHAR(200) NOT NULL,
	json	text NOT NULL
);

CREATE TABLE workerendpoint(
	id	    serial PRIMARY KEY,
	name    VARCHAR(200) NOT NULL,
	disabled boolean NOT NULL DEFAULT false
);

CREATE TABLE workerendpoint_headers(
	id	    serial PRIMARY KEY,
	workerendpoint_id integer NOT NULL,
	value    VARCHAR(500) NOT NULL,
	constraint fk_weh_workerendpointid foreign key (workerendpoint_id) REFERENCES workerendpoint (id)
);

