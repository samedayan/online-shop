create database customers;

create table public."User"
(
    "Id"               integer generated always as identity,
    "Name"             varchar(50) not null,
    "LastName"         varchar(50) not null,
    "Phone"            varchar(15),
    "Email"            varchar(50),
    "Address"          text,
    "CreatedDate"      timestamp   not null,
    "LastModifiedDate" timestamp
);

--------------------------------------------------------------------

create database orders;

create table if not exists public."Order"
(
    "Id"         integer generated always as identity,
    "CustomerId" integer not null,
	"StatusId" integer not null,
	"Description"      text,
	"CreatedDate"      timestamp   not null,
    "LastModifiedDate" timestamp
);

create table if not exists public."OrderProduct"
(
    "Id"         integer generated always as identity,
	"OrderId"  integer not null,
    "ProductId"  integer not null,
	"Quantity"  integer not null,
	"Description"      text,
	"CreatedDate"      timestamp   not null,
    "LastModifiedDate" timestamp
);
--------------------------------------------------------------------

create database products;

create table public."Product"
(
    "Id"               integer generated always as identity,
    "Code"             varchar(20)  not null,
    "Name"             varchar(150) not null,
    "CategoryId"       integer      not null,
    "Description"      text,
    "ImagePath"        varchar(100),
    "CreatedDate"      timestamp,
    "LastModifiedDate" timestamp
);

--------------------------------------------------------------------

