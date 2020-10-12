
desc Transactions


create Table Transactions(T_ID numeric(10) Not Null,
Customer_ID number(10) Not null,Acc_No number(10) Not null,
T_Type varchar2(20),T_Amount number(20,2),T_DateTime date,
primary key(T_ID),foreign key(Acc_No) references Account_Details(Acc_No));


create or replace trigger customer_transaction_tgr
after update
on Account_Details
for each row
declare


