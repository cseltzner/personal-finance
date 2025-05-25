create table if not exists l_TransactionCategory
(
    RowID            serial primary key,
    CategoryID       varchar(50) unique not null, -- The mcc code
    ParentCategory   varchar(50),
    Category         varchar(255),
    ShortName        varchar(255),
    Description      varchar(1000),
    CreatedDate      timestamp default current_timestamp,
    CreatedBy        varchar(50),
    VoidDate         timestamp
);

create index l_transactioncategory_catid_idx on l_transactioncategory(categoryid);

create table if not exists e_Account
(
    RowID         serial primary key,
    AccountNumber varchar(255) not null unique,
    Entity        varchar(50) not null,
    AccountName   varchar(255),
    Description   varchar(1000),
    Type          varchar(50), -- e.g. Checking, Savings, Credit Card, etc.
    CreatedDate   timestamp default current_timestamp,
    CreatedBy     varchar(50),
    VoidDate      timestamp
);

create table if not exists t_Transaction
(
    RowID         serial primary key,
    Entity        varchar(50) not null,
    TransactionID varchar(255),
    Date          timestamp,
    Type          varchar(255),
    Origin        varchar(255),
    Description   varchar(1000),
    Account       varchar(255) references e_Account (AccountNumber),
    Category      varchar(50) references l_TransactionCategory (CategoryID),
    Amount        numeric(19, 4),
    Note          varchar(1000),
    Source        varchar(255),
    CreatedDate   timestamp default current_timestamp,
    CreatedBy     varchar(50),
    VoidDate      timestamp
);