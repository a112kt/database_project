--CRUD and Search 
use Hotel
--Create a new Guest to our Hotel using Procedure and Transaction
GO
CREATE OR ALTER  PROC SPCreateGuest 
@Fname varchar(25), 
@Lname varchar(25), 
@Email varchar(40),
@address varchar(100),
@phone_num varchar(15)
as
BEGIN TRANSACTION;
BEGIN TRY
    -- input new Guest
Begin
    insert into Guest(Fname,Lname,Email,address,Phone_Number)
	values(@Fname, @Lname, @Email,@address,@phone_num)
	print 'Guest add successfully'
end
    -- confirm operation
    COMMIT;
    PRINT 'Transaction committed successfully. Data inserted into Guest table.';
END TRY
BEGIN CATCH
    -- in error case
   ROLLBACK;
   PRINT 'Transaction rolled back. An error occurred.';
   RAISERROR('Guest already exits', 15,1);
END CATCH;


--using the stored procedure to add guest 
Exec SPCreateGuest 'ASf','Raa','hggfdg','NADA','01222'

select *
from Guest
-------------------------------------------End Of Create-------------------------------------------------
--Read data of the Available room using View 
GO
create or alter View VGetDataOfRoom
as
select Room_id,[Type],[Status],[Price]
from Room
where [Status] = 'Available'

GO
select * from VGetDataOfRoom

-------------------------------------------End Of Read-------------------------------------------------

--Update Room Using Stored Procedure
GO
create or alter Procedure SpUpdateRoom(@id int,
@Type varchar(20)=null,
@Status varchar(20)=null,
@price float =null)
as
Begin
Update Room 
set 
   [Type] =coalesce(@Type,[Type]),
   [Status] =coalesce(@Status,[Status]),
   price = coalesce(@price,price)
where Room_id = @id
print 'Room Update Successfully'
End

exec SpUpdateRoom @id=2,@Type='Double' ,@Status='Occupied',@price=64;

select*
from Room

-------------------------------------------End Of Update-------------------------------------------------

--Delete Guest using stored procedure and transaction
GO
CREATE OR ALTER PROC DeleteReservationByRoomId(@id INT)
AS
BEGIN
	 delete from Room_Reservation 
	 where Room_Id = @id
END;
GO

Exec DeleteReservationByRoomId 1
------------------
--To prevent Delete Using Triggers
GO
create or alter Trigger trg_afterReservationDelete 
on Room_Reservation 
after delete
as
Begin  
    Update Room 
	set Status = 'Avaliable'
	where Room_Id IN (SELECT Room_Id FROM DELETED);
END;

-------------------------------------------End Of Delete-------------------------------------------------

--search By procedure
--search by guest id to get its data 
GO
create or alter proc SPGetGuestRoomById(@id int)
as
begin 
   select G.Gu_id as Id, CONCAT_WS(' ', G.Fname,G.Lname) as FullName , RO.Type,RO.Status,R.CheckIn_Date,R.Checkout_Date
   From Guest G 
   join Reservation R on G.GU_Id = R.Gu_id 
   join Room_Reservation RR on R.Res_id = RR.res_id 
   join Room RO on RR.Room_Id = ro.Room_id
   where G.GU_id = @id
end

exec SPGetGuestRoomById 10

--search by letter or the whole name on Guest table 
GO
create or alter Function FNgetGuestByLetter(@letter varchar(50))
returns @GestName table (FullName varchar(100))
as
begin
   insert into @GestName
   select CONCAT_WS(' ', G.Fname,G.Lname) 
   from Guest G
   where  Fname  like '%' + @letter + '%'
return 
end

GO
select *
from FNgetGuestByLetter('i')

--search by type on room table 
GO
CREATE OR ALTER PROCEDURE SPgetAvailableRoomByType(@type VARCHAR(20))
AS
BEGIN
   SELECT Room_Id, Type, Price 
   FROM Room R
   WHERE Type = @type AND Status = 'Available'
   ORDER BY Price ASC
   RETURN
END
GO

EXEC SPgetAvailableRoomByType 'Suite'
-------------------------------------------End Of Search-------------------------------------------------