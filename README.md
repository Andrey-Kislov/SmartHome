```
psql -U postgres
CREATE DATABASE smarthome;

SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE pid <> pg_backend_pid() AND datname = 'smarthome';
DROP DATABASE smarthome;```