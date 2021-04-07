DROP TABLE IF EXISTS request;
DROP TABLE IF EXISTS user;
CREATE TABLE user
(
    id       INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    email    VARCHAR(128) UNIQUE,
    password VARCHAR(128),
    name     VARCHAR(128)
);
CREATE TABLE request
(
    id           INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    title        VARCHAR(128),
    description  VARCHAR(1024),
    price        INT,
    meetingType  VARCHAR(128),
    meetingSpecs VARCHAR(128),
    userId       INT,
    CONSTRAINT FK_REQUEST_USER FOREIGN KEY (userId) REFERENCES user (id)
);