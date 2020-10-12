desc transactions

CREATE SEQUENCE TransactionId
  START WITH 100
  INCREMENT BY 1
  CACHE 100;

alter table transactions add constraint T_ID_pk primary key(T_ID);
  

  CREATE OR REPLACE TRIGGER InputTransactionId
  BEFORE INSERT ON Transactions
  FOR EACH ROW
BEGIN
  SELECT TransactionId.nextval
	INTO :new.T_ID
	FROM dual;
END;

drop trigger InputTransactionId

CREATE OR REPLACE TRIGGER customer_transaction_trg
	AFTER UPDATE 
	ON Account_Details -- CAN U SHOW ME GTHTIS TABEL?
	FOR EACH ROW    
DECLARE
   --amount number;

   
BEGIN

if(updating) then
end if;
   -- determine the transaction type
  
   --if (:New.Balance> :Old.Balance) then
		-- amount:=New.Balance - Old:Balance;
	 	 
		-- INSERT INTO TRANSACTIONS(CUSTOMER_ID,ACC_NO,T_TYPE,T_AMOUNT,T_DATETIME)
		-- VALUES(:Old.Customer_ID,:Old.Acc_No,'Credit',amount,Sysdate);
   --else
		-- amount:=:Old.Balance - :New.Balance;
		-- INSERT INTO TRANSACTIONS(CUSTOMER_ID,ACC_NO,T_TYPE,T_AMOUNT,T_DATETIME)
		-- VALUES(:Old.Customer_ID,:Old.Acc_No,'Debit',amount,Sysdate);
   --End if;

   eXCEPTION 
	WHEN OTHERS
		THEN raise_application_error('Error')
   
END;



SHOW ERRORS TRIGGER trigger_name;





create or replace test_trg
after update on account_details
for each row
begin
if(updating)
then
end if;
end;

select * from Account_DEtails;
   
    show errors procedure <customer_transaction_trg> ;
sql>>show errors



VALUES('CUSTOMERS', l_transaction, USER, SYSDATE);


select * from Transactions;





CREATE OR REPLACE TRIGGER transaction_trg
AFTER UPDATE ON account_details
FOR EACH ROW
DECLARE
  v_t_type varchar2(20);
  amount number(20,2);
BEGIN
  CASE 
    WHEN old.balance > :new.balance 
    THEN v_t_type := 'Debit';
        amount:=:old.balance - :new.balance;
    
    WHEN :old.balance < :new.balance THEN 
    v_t_type := 'Credit';
    amount:=:new.balance - :old.balance;
    
    WHEN ELSE THEN v_t_type := 'none';
  END CASE;


  INSERT INTO TRANSACTION (T_ID, CUSTOMER_ID, ACC_NO, T_TYPE,T_Amount,T_DateTime)
    VALUES (t_id_seq.nextval, :new.customer_id, :new.acc_no, t_v_type,amount, Sysdate);

END transaction_trg;
/

SHOW ERRORS TRIGGER transaction_trg;







-- Correct trigger

create or replace trigger Transaction_trg
after update on Account_details
for each row
declare
pragma autonomous_transaction;
begin
 if(:New.Balance <> :Old.Balance) then
   if(:New.Balance < :old.Balance) then
   
    insert into transactions(T_ID,Customer_ID,Acc_No,T_Type,T_Amount,T_DateTime) values(TransactionId.nextval,:old.Customer_Id,:Old.Acc_No,'Debit',(:old.Balance-:New.Balance),sysdate);
    commit ;
    end if;
   if(:New.Balance > :old.Balance) then
    insert into transactions(T_ID,Customer_ID,Acc_No,T_Type,T_Amount,T_DateTime) values(TransactionId.nextval,:old.Customer_Id,:Old.Acc_No,'Credit',(:New.Balance-:OLd.Balance),sysdate);
    commit;
    end if;
end if;
end;
