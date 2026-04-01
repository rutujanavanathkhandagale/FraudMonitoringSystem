delete from Transactions;
dbcc checkident ('Transactions',reseed,0);