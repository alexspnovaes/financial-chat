#!/usr/bin/env bash
# Wait for database to startup 
sleep 20
./opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 27@89Hl87 -i setup.sql