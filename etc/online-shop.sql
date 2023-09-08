create database customers;
create database orders;
create database products;

---

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

