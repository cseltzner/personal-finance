create table if not exists l_TransactionCategory
(
    RowID          serial primary key,
    CategoryID     text unique not null, -- The mcc code
    ParentCategory text,
    Category       text,
    ShortName      text,
    Description    text,
    CreatedDate    timestamp default current_timestamp,
    CreatedBy      text,
    VoidDate       timestamp
);

create index if not exists l_transactioncategory_catid_idx on l_transactioncategory (categoryid);

create table if not exists e_Account
(
    RowID         serial primary key,
    AccountNumber text    not null,
    UserID        integer not null references e_User (RowID),
    AccountName   text,
    Description   text,
    Type          text, -- e.g. Checking, Savings, Credit Card, etc.
    CreatedDate   timestamp default current_timestamp,
    CreatedBy     text,
    VoidDate      timestamp
);

create unique index if not exists e_account_accountnumber_active_idx
    on e_Account (AccountNumber, UserID)
    where VoidDate is null;

create table if not exists t_Transaction
(
    RowID         serial primary key,
    UserID        integer not null references e_User (RowID),
    TransactionID text,
    Date          timestamp,
    Type          text,
    Origin        text,
    Description   text,
    AccountID     integer references e_Account (RowID),
    Category      text references l_TransactionCategory (CategoryID),
    Amount        numeric(19, 4),
    Note          text,
    Source        text,
    CreatedDate   timestamp default current_timestamp,
    CreatedBy     text,
    VoidDate      timestamp
);