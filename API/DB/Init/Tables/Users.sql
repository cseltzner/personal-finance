create table if not exists e_User
(
    RowID        serial primary key,
    Username     text not null,
    Email        text not null,
    PasswordHash text not null,
    FirstName    text,
    LastName     text,
    Phone        text,
    UserRole     text, -- e.g. Admin, User
    CreatedDate  timestamp default current_timestamp,
    CreatedBy    text,
    VoidDate     timestamp
);

create unique index if not exists e_user_username_email_idx
    on e_User (Username, Email)
    where VoidDate is null;

create table if not exists l_UserSettings
(
    RowID       serial primary key,
    SettingName text unique not null,
    Description text,
    CreatedDate timestamp default current_timestamp,
    CreatedBy   text
);

create table if not exists e_UserSettings
(
    RowID        serial primary key,
    UserID       integer not null references e_User (RowID),
    SettingID    integer references l_UserSettings,
    SettingValue text,
    CreatedDate  timestamp default current_timestamp,
    CreatedBy    text
);

create unique index if not exists e_usersettings_userid_SettingID_idx
    on e_UserSettings (UserID, SettingID)