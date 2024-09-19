"# SupermarketCheckout" 

After cloning the code, follow the below steps to set up the database:

1. Right click on SupermarketCheckout.Database > Rebuild
2. View > SQL Server Object Explorer
3. SQL Server > (localdb)\MSSQLLocalDb... > Databases > System Databases
4. Right click on master > Publish Data-tier Application...
5. Browse > ...\SupermarketCheckoutAPI\Database\bin\Debug > SupermarketCheckout.Database.dacpac > Open
6. Publish
7. Right click on master > New Query > Paste from SupermarketCheckout.Database > Scripts > InsertData.sql
8. Run application by clicking on https