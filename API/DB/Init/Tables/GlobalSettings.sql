create table if not exists e_GlobalSettings (
    SettingName text primary key,
    Description text,
    SettingValue text,
    CreatedDate timestamp default current_timestamp,
    CreatedBy text
);

insert into e_GlobalSettings (SettingName, Description, SettingValue, CreatedBy)
select 'InitialDataSeededYN', 'Indicates if the initial data has been seeded into the database', '0', 'System'
where not exists (select 1 from e_GlobalSettings where SettingName = 'InitialDataSeededYN');