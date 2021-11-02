USE [PryVata];
GO

--setting usertypes

set identity_insert [UserType] on
insert into [UserType] (Id, [Name]) VALUES (1, 'Admin'), (2, 'Regular');
set identity_insert [UserType] off

--DUMMY DATA

--Facility

set identity_insert [Facility] on
insert into [Facility] (Id, FacilityName, Address, City, State, ZipCode, isDeleted) VALUES (1, 'Central Perk Medical Center', '123 Baldwin Street', 'New York City', 'NY', 28842, 0);
insert into [Facility] (Id, FacilityName, Address, City, State, ZipCode, isDeleted) VALUES (2, 'Friends Medical Center', '123 Friendly Road', 'New York City', 'NY', 28842, 0);
insert into [Facility] (Id, FacilityName, Address, City, State, ZipCode, isDeleted) VALUES (3, 'Met Gala University Medical Center', '123 Bayer Road', 'New York City', 'NY', 28842, 0);
insert into [Facility] (Id, FacilityName, Address, City, State, ZipCode, isDeleted) VALUES (4, 'Hello Hospital', '123 Rocky Lane', 'New York City', 'NY', 28842, 0);
insert into [Facility] (Id, FacilityName, Address, City, State, ZipCode, isDeleted) VALUES (5, 'MilkTea University Hospital', '123 Coconut Bubble Way', 'New York City', 'NY', 28842, 0);
set identity_insert [Facility] off


--User

set identity_insert [User] on
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (1, 'Rachel Green', 'rachel@green.com', '7jET0TCGZNNJHwYsMlyuDESvYLS2', 2, 3, 1);
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (2, 'Ross Geller', 'ross@geller.com', 'qQzTjWJoM3YU2wgUUG8bKERZPit1', 2, 2, 1);
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (3, 'Monica Geller', 'monica@geller.com', 'BNxfNDfalHYwMDp2xqsjXI1b80K3', 2, 1, 1);
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (4, 'Chandler Bing', 'chandler@bing.com', '6HEjc1XOxkWEaeupluCSiLmCy0F3', 1, NULL, 1);
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (5, 'Phoebe Buffay', 'phoebe@buffay.com', 'JxYbc7vcooSJmgbgWtnNicBBq7G3', 2, 2, 1);
insert into [User] (Id, [FullName], Email, FirebaseUserId, UserTypeId, FacilityId, isActive) values (6, 'Joey Tribbiani', 'joey@tribbiani.com', '3hZJ8GS2GRYHYChcDLPEqkdrotG3', 1, NULL, 1);
set identity_insert [User] off

--***********DBRA TABLES*************

--Method

set identity_insert [MethodType] on 
insert into [MethodType] (Id, Method, MethodValue) VALUES (1, 'Unauthorized internal acquisition, access or internal use/disclosure', 1), (2, 'Verbal disclosure', 1),
(3, 'View only', 1), (4, 'Paper', 2), (5, 'Electronic', 3);
set identity_insert [MethodType] off

--Recipient

set identity_insert [RecipientType] on
insert into [RecipientType] (Id, Recipient, RecipientValue) VALUES (1, 'Business Associate of the Organization', 1), (2, 'Another Covered Entity', 1),
(3, 'Internal Workforce Member', 1), (4, 'Wrong Payor/Insurance Company', 2), (5, 'Unauthorized family member or other patient', 2), (6, 'Unknown Recipient – Information lost or stolen', 3),
(7, 'A company (non-covered entity), member of general public, media, etc.', 3);
set identity_insert [RecipientType] off

--Circumstance

set identity_insert [Circumstance] on
insert into [Circumstance] (Id, Circumstance, CircumstanceValue) VALUES (1, 'Unintentional disclosure', 1), (2, 'Intentional use/access without authorization', 1), 
(3, 'Intentional disclosure witihout authorization', 2), (4, 'Loss or Theft', 2), (5, 'Using false pretense to obtain or disclose', 3),
(6, 'Obtain for personal gain or with malicious intent to cause harm', 3), (7, 'Hacked or targeted data theft', 3);
set identity_insert [Circumstance] off

--Disposition of Information

set identity_insert [Disposition] on
insert into [Disposition] (Id, Disposition, DispositionValue) VALUES (1, 'Original, complete information returned', 1), (2, 'Information properly destroyed (Written attestation/assurance obtained)', 1),
(3, 'Information could NOT be reasonably retained', 1), (4, 'Information properly destroyed (No attestation/assurance obtained)', 2),
(5, 'Electronically deleted', 2), (6, 'Disclosed to Media', 3), (7, 'Unable to retrieve or unsure of location/disposition', 3),
(8, 'High probability of re-disclosure or suspected re-disclosure', 3);
set identity_insert [Disposition] off

--Information

set identity_insert [Information] on 
insert into [Information] (Id, InformationType, InformationValue) VALUES (1, 'First Name', 3), (2, 'Last Name', 3), (3, 'Address', 1),
(4, 'Date of Birth', 3), (5, 'Social Security Number', 4), (6, 'Drivers License Number', 4), (7, 'Type of Insurance', 1), (8, 'PolicyId', 4),
(9, 'Group Id', 2), (10, 'Payment Information (Debit, Credit, Bank Acct)', 4), (11, 'Diagnosis (Non-sensitive)', 2), (12, 'Diagnosis (Sensitive)', 4),
(13, 'Medications (Non-sensitive)', 2), (14, 'Medications (Sensitive)', 4), (15, 'Test Results (Non-sensitive)', 2), (16, 'Test Results (Sensitive)', 4),
(17, 'Other Care', 1);
set identity_insert [Information] off

--Exception

set identity_insert [Exception] on
insert into [Exception] (Id, Exception) VALUES (1, 'Good faith, unintentional acquisition, access or use of PHI by employee/workforce'),
(2, 'Inadvertent disclosure to another authorized person within the entity or OHCA'), (3, 'Recipient could not reasonably have retained the data'), (4, 'Data is limited to limited data set that does not include dates of birth or zip codes'),
(5, 'No exceptions met');
set identity_insert [Exception] off

--Controls

set identity_insert [Controls] on
insert into [Controls] (Id, Controls, ControlsValue) VALUES (1, 'Data Wiped', 1), (2, 'Encrypted/Destroyed – Non-NIST compliant', 1), (3, 'Physical/Policy Controls', 1),
(4, 'Password Compliant (not compromised)', 2), (5, 'Password Protected (compromised)', 3), (6, 'No Controls/Unencrypted', 3), (7, 'Other - Provide explanation in the Notes portion', 3);
set identity_insert [Controls] off

--Incident

set identity_insert [Incident] on
insert into [Incident] (Id, AssignedUserId, Title, [Description], DateReported, DateOccurred, FacilityId, Confirmed, Reportable, DBRAId) VALUES
(1, 4, 'Nurse provided wrong discharge papers', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '10/01/2021', '9/25/2021', 1, 1, 0, NULL),
(2, 4, 'Patient was given incorrect wristband', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '10/31/2021', '10/25/2021', 2, 1, 0, NULL),
(3, 4, 'HIM provided incorrect patient information', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '08/12/2021', '05/31/2021', 3, 1, 1, 3),
(4, 1, 'Faxed incorrect records', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '01/15/2021', '12/25/2020', 1, 1, 1, 4),
(5, 2, 'Patient received a call from the hospital about an unscheduled appointment', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '10/10/2021', '10/02/2021', 2, NULL, NULL, NULL),
(6, 3, 'Incorrect paperwork during transfer', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '11/01/2021', '10/25/2021', 4, NULL, NULL, NULL),
(7, 6, 'Patient complaint', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '10/28/2021', '10/28/2021', 2, 0, NULL, NULL),
(8, 5, 'Privacy allegation', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '09/30/2021', '9/25/2021', 5, 1, 1, 2),
(9, 6, 'Received anonymous call', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '11/01/2021', '10/25/2021', 4, NULL, NULL, NULL),
(10, 1, 'Patient complaint', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '11/01/2021', '10/25/2021', 3, NULL, NULL, NULL);
set identity_insert [Incident] off

--Notes

set identity_insert [Notes] on
insert into [Notes] (Id, CreateDateTime, [Description], ImageUrl, IncidentId) VALUES (1, '09/01/2021', 'Did she do it? Interviews are coming up', NULL, 1), (2, '09/01/2021', 'Accesses being taken', NULL, 1);
set identity_insert [Notes] off

--Patient
set identity_insert [Patient] on
insert into [Patient] (Id, PatientNumber, FirstName, LastName) VALUES (1, 12345678, 'Harry', 'Potter'),(2, 12345678, 'Ron', 'Weasley'),(3, 12345678, 'Ginny', 'Weasley'),(4, 12345678, 'Fred', 'Weasley'),
(5, 12345678, 'George', 'Weasley'),(6, 12345678, 'Peter', 'Pettigrew'),(7, 12345678, 'Hermione', 'Granger'),(8, 12345678, 'Harry', 'Potter'),(9, 12345678, 'Harry', 'Potter'),(10, 12345678, 'Harry', 'Potter'),(11, 12345678, 'Cedric', 'Diggory'),
(12, 12345678, 'Albys', 'Dumbledore');
set identity_insert [Patient] off

--PatientIncident

set identity_insert [PatientIncident] on
insert into [PatientIncident] (Id, IncidentId, PatientId) VALUES (1, 1, 1), (2, 2, 2), (3, 3, 3), (4, 4, 4), (5, 5, 5), (6, 6, 6), (7, 7, 7), (8, 8, 8), (9, 9, 9), (10, 10, 10),
(11, 1, 11), (12, 1, 12);
set identity_insert [PatientIncident] off

--DBRATest 

set identity_insert [DBRA] on
insert into [DBRA] (Id, ExceptionId, UserCompleteId, MethodId, RecipientId, CircumstanceId, DispositionId, IncidentId, RiskValue) VALUES
(1, 5, 2, 1, 3, 4, 3, 3, 15), (2, 5, 2, 1, 3, 4, 3, 4, 15), (3, 5, 2, 1, 3, 4, 3, 8, 15);
set identity_insert [DBRA] off

--DBRAInformation

set identity_insert [DBRAInformation] on
insert into [DBRAInformation] (Id, DBRAId, InformationId) VALUES (1, 1, 2), (2, 1, 4), (3, 1, 6),(4, 2, 2), (5, 2, 4), (6, 2, 6),(7, 3, 2), (8, 3, 4), (9, 3, 6);
set identity_insert [DBRAInformation] off

--DBRAControls

set identity_insert [DBRAControls] on 
insert into [DBRAControls] (Id, DBRAId, ControlsId) VALUES (1, 1, 2), (2, 1, 3);
set identity_insert [DBRAControls] off