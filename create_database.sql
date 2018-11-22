drop table if exists wi;
create table wi as select distinct id,word,idx from words
where tablename='posts' and what='title' and word ~* '[a-z]';
---------------------------
-- DROP TABLES
----------------------------
DROP TABLE IF EXISTS history;
DROP TABLE IF EXISTS tags;
DROP TABLE IF EXISTS favorites;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS comment;
DROP TABLE IF EXISTS post;
DROP TABLE IF EXISTS qauser;
DROP TABLE IF EXISTS wordindextitle;
DROP TABLE IF EXISTS wordindexbody;

-----------------------------------------------------------------------------------
-- Vi laver tables der skal bruges i vores egen database
-- Post er en generel tabel 
-- Question og answer er specialiserede post tabeller
-----------------------------------------------------------------------------------
CREATE TABLE public.post (
    id integer PRIMARY KEY,
	title text,
	posttype integer ,
	parentid integer,
	acceptedanswerid integer,
	creationdate timestamp without time zone,
    score integer,
    body text,
	closeddate timestamp without time zone,
	ownerid integer
);

CREATE TABLE public.comment (
    id integer PRIMARY KEY,
	postid integer,
    body text,
    score integer,
  	creationdate timestamp without time zone,
    authorid integer
);

CREATE TABLE public.qauser (
    id integer PRIMARY KEY,
    displayname text,
    creationdate timestamp without time zone,
    location text,
	age integer
);

CREATE TABLE public.tags (
	tag text PRIMARY KEY
); 

CREATE TABLE public.posttags (
	id integer, 
	tag text,
    primary key (id, tag)
); 

CREATE TABLE postlinks (
	id integer,
    postlinkid integer,
    primary key (id, postlinkid)
);

CREATE TABLE wordindextitle as select distinct id, word, idx 
from words 
where tablename='posts' and what='title' and word ~* '[a-z]';

CREATE TABLE wordindexbody as select distinct id, word, sen, idx 
from words
where tablename='posts' and what='body' and word ~* '[a-z]';

-----------------------------------------------------
-- Data fra universal indsættes i egne tabeller
-----------------------------------------------------
INSERT INTO post(id, posttype, parentid, acceptedanswerid, score, body, closeddate, title, ownerid, creationdate)
SELECT DISTINCT id, posttypeid, parentid, acceptedanswerid, score, body, closeddate, title, ownerid, creationdate
FROM posts_universal; 

INSERT INTO comment(id, postid, body, score, creationdate, authorid)
SELECT commentid, postid, commenttext, commentscore, commentcreatedate, authorid
FROM comments_universal;

INSERT INTO qauser(id,displayname,creationdate,location,age)
SELECT DISTINCT authorid, authordisplayname, authorcreationdate, authorlocation, authorage
FROM comments_universal
UNION
SELECT DISTINCT ownerid, ownerdisplayname, ownercreationdate, ownerlocation, ownerage
FROM posts_universal;

-------------------------------------------------------------
-- ACCEPTED ANSWER NOT EXISTING IN POSTS
-------------------------------------------------------------
UPDATE post 
SET acceptedanswerid = NULL 
WHERE acceptedanswerid  not in (select id from post);

------------------------------------------------------------------------------------------------------------
-- FRAMEWORK for SOVA-app
------------------------------------------------------------------------------------------------------------
CREATE TABLE public.users (
	username text PRIMARY KEY, 
	password text
);

CREATE TABLE public.history (
	username text,
	creation_date timestamp without time zone,
	search_text text,
	primary key(username, creation_date)
);

CREATE TABLE public.favorites (
	postid integer,
	username text,
	note text,
	primary key(postid, username)
);
----------------------------------------------------------------------------------------
-- Skaber primary keys & foreign keys til tables efterfølgende for overskuelig læsning
----------------------------------------------------------------------------------------
ALTER TABLE history
ADD FOREIGN KEY (username) REFERENCES users (username);

ALTER TABLE favorites
ADD FOREIGN KEY (postid) REFERENCES post(id),
ADD FOREIGN KEY (username) REFERENCES users(username);

ALTER TABLE post
ADD FOREIGN KEY (parentid) REFERENCES post(id),
ADD FOREIGN KEY (acceptedanswerid) REFERENCES post(id),
ADD FOREIGN KEY (ownerid) REFERENCES qauser(id);

ALTER TABLE comment
ADD FOREIGN KEY (postid) REFERENCES post(id),
ADD FOREIGN KEY (authorid) REFERENCES qauser(id);


CREATE OR REPLACE FUNCTION "public"."searchSova"("sinput" bpchar, "loggedusername" text)
  RETURNS TABLE("postid" int4, "posttitle" text, "postscore" int4, "postbody" text) AS $BODY$
BEGIN

		INSERT INTO history (username, creation_date, search_text)
		VALUES(loggedusername,now(),sinput);

		RETURN QUERY
		SELECT id, title, score, body
		FROM post
		WHERE body ILIKE concat('%',sinput,'%') OR title ILIKE concat('%',sinput,'%')
		ORDER BY score desc;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
  
  
CREATE OR REPLACE FUNCTION "public"."createUser"("inputusername" text, "inputpassword" text)
  RETURNS "pg_catalog"."void" AS $BODY$
BEGIN
		INSERT INTO users(username, password)
		VALUES (inputusername, inputpassword);
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

  
  CREATE OR REPLACE FUNCTION "public"."markFavorite"("chosenpostid" int4, "loggedusername" text, "mypersonalnote" text)
  RETURNS "pg_catalog"."void" AS $BODY$
BEGIN
	IF EXISTS(SELECT postid, username FROM favorites WHERE postid = chosenpostid and username = loggedusername)
	THEN 
		DELETE FROM favorites WHERE postid = chosenpostid and username = loggedusername;
	ELSE
		INSERT INTO favorites(postid, username, note)
		VALUES (chosenpostid, loggedusername, mypersonalnote);
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  
CREATE OR REPLACE FUNCTION "public"."split_tags"()
  RETURNS "pg_catalog"."void" AS $BODY$
declare
    counter integer:= 1;
    counter_break integer = 0;
    rec record;
begin
    for rec in select tags from posts_universal
    loop
            if rec.tags is not null then
            while counter_break = 0 loop
                if split_part(rec.tags, '::', counter) like '' then
                    counter_break = 1;
                elseif split_part(rec.tags, '::', counter) in (select tag from tags) then
                    counter := counter+1;
                else
                    insert into tags
                        values(split_part(rec.tags, '::', counter));
                        counter := counter+1;
                end if;
            end loop;
                        counter_break = 0;
                        counter = 1;
            end if;
    end loop;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  
 select split_tags();
 
 CREATE OR REPLACE FUNCTION "public"."assign_tags"()
  RETURNS "pg_catalog"."void" AS $BODY$
declare
    counter integer:= 1;
    counter_break integer = 0;
    rec record;
begin
    for rec in select tags, id from posts_universal
    loop
            if rec.tags is not null then
            while counter_break = 0 loop
                if split_part(rec.tags, '::', counter) like '' then
                    counter_break = 1;
                elseif split_part(rec.tags, '::', counter) in (select tag from tags) then
                                        insert into posttags
                                            values(rec.id, split_part(rec.tags, '::', counter))
                                            on conflict do nothing;
                    counter := counter+1;
                end if;
            end loop;
                        counter_break = 0;
                        counter = 1;
            end if;
    end loop;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
 
select assign_tags();

insert into postlinks(id, postlinkid)
    select id, linkpostid
    from posts_universal
    where linkpostid is not null
        and linkpostid in (select id from post)
on conflict do nothing;

--DROP TABLE IF EXISTS posts_universal;
--DROP TABLE IF EXISTS comments_universal;
DROP FUNCTION IF EXISTS split_tags;
DROP FUNCTION IF EXISTS assign_tags;
								      
CREATE OR REPLACE FUNCTION "public"."exactMatchSova"("sinput" text, "loggedusername" text)
  RETURNS TABLE("postid" int4, "posttitle" text, "postscore" int4, "postbody" text) AS $BODY$
DECLARE
   counter integer := 1;
   inputArray text[];
	 word text;
	 q text :='';
	 max_counter integer;
BEGIN

		INSERT INTO history (username, creation_date, search_text)
		VALUES(loggedusername,now(),sinput);
		
		SELECT regexp_split_to_array(sinput, '\s+')
		INTO inputArray;
		max_counter := array_length(inputArray,1);
		
		q:= 'select id, title, score, body from post where id in
								(select id from words where word = ''';
		q := q || inputArray[1];
		WHILE counter < max_counter
		LOOP
			counter := counter + 1;
			q := q || ''') and id in (select id from words where word = ''';
			q := q || inputArray[counter];
		END LOOP;
		q := q || ''');';
		RAISE NOTICE '%', q;
		RETURN QUERY EXECUTE q;
		
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
								    
CREATE OR REPLACE FUNCTION "public"."wordToWords"("winput" text, "loggedusername" text)
  RETURNS TABLE("word" varchar, "word_f" numeric) AS $BODY$
DECLARE
	
BEGIN

INSERT INTO history (username, creation_date, search_text)
VALUES(loggedusername,now(),winput);
	
RETURN QUERY SELECT distinct wordfrequency.word, sum(wordfrequency) as word_f
	     FROM wordfrequency
	     WHERE id in (SELECT id  
			  FROM wordfrequency
			  WHERE wordfrequency.word = winput)
	     GROUP BY wordfrequency.word
	     ORDER BY word_f DESC;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000
								      
drop table if exists tfidf_idx;
create table tfidf_idx ( 
	id int not null,
	word text not null, 
	tf float, 
	idf float, 
	tfidf float,
	primary key (id, word)
); 
								      
create index wib_word on wordindexbody(word);
create index wib_id on wordindexbody(id);
create index wib_idx on wordindexbody(idx);
create index wib_sen on wordindexbody(sen);

create index wit_word on wordindextitle(word);
create index wit_id on wordindextitle(id);
create index wit_idx on wordindextitle(idx);
								      
create table wordfrequency as
select id, count (word) as wordfrequency, word 
from wordindexbody 
group by id, word; 
								  

CREATE TABLE wordindex as select distinct id, word, what, sen, idx 
from words
where tablename='posts' and word ~* '[a-z]';

create index wi_word on wordindex(word);
create index wi_id on wordindex(id);
create index wi_idx on wordindex(idx);
create index wi_sen on wordindex(sen);

insert into tfidf_idx(select id, word from wordindex) on conflict do nothing;

update tfidf_idx set tf = 
	(select count(*) from wordindex wi where wi.id = tfidf_idx.id and wi.word = tfidf_idx.word group by word);
								      
/*Sidste version (oprindelige er uden "lower()"):*/
drop table if exists frequencyindex;
create table frequencyindex ( 
	id int not null,
	word text not null, 
	tf float, 
	df float, 
	dw float,
	tfidf float,
	primary key (id, word)
); 

/*CREATE TABLE wordindex as select distinct id, word, what, sen, idx 
from words
where tablename='posts' and word ~* '[a-z]';*/

create index wi_word on wordindex(word);
create index wi_id on wordindex(id);
create index wi_idx on wordindex(idx);
create index wi_sen on wordindex(sen);

insert into frequencyindex(select id, lower(word) as word from wordindex) on conflict do nothing;

update frequencyindex set tf = 
	(select count(*) from wordindex wi where wi.id = frequencyindex.id and wi.word = frequencyindex.word group by word);
	
create index frequencyindex_word on frequencyindex(word);
create index frequencyindex_id on frequencyindex(id);

CREATE TABLE documentfrequency as select distinct lower(word) as word, count(distinct id) 
from frequencyindex
group by word;

CREATE TABLE documentwords as select distinct id, count(distinct lower(word)) 
from frequencyindex
group by id;

create index df_word on documentfrequency(word);
create index df_count on documentfrequency(count);
update frequencyindex set df = (select count from documentfrequency where documentfrequency.word = frequencyindex.word);

create index dw_id on documentwords(id);
create index dw_count on documentwords(count);
update frequencyindex set dw = (select count from documentwords where frequencyindex.id = documentwords.id);

create index idx_df on frequencyindex(df);
create index idx_dw on frequencyindex(dw);
create index idx_tf on frequencyindex(tf);

update frequencyindex set tfidf = round((log(1+(tf/dw))*(1/df)*1000)::numeric,2);
