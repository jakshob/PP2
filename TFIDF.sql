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

CREATE TABLE wordindex as select distinct id, lower(word) as word, what, sen, idx 
from words
where tablename='posts' and word ~* '[a-z]';

create index wi_word on wordindex(word);
create index wi_id on wordindex(id);
create index wi_idx on wordindex(idx);
create index wi_sen on wordindex(sen);

insert into frequencyindex(select id, lower(word) as word from wordindex) on conflict do nothing;

create index frequencyindex_word on frequencyindex(word);
create index frequencyindex_id on frequencyindex(id);

update frequencyindex set tf = 
	(select count(*) from wordindex wi where wi.id = frequencyindex.id and wi.word = frequencyindex.word group by word);
	

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
