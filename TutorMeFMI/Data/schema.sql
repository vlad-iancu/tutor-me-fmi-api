DROP TABLE IF EXISTS offer;
DROP TABLE IF EXISTS request;
DROP TABLE IF EXISTS subject;
DROP TABLE IF EXISTS user;
CREATE TABLE user(
                     id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
                     email VARCHAR(128) UNIQUE,
                     password VARCHAR(128),
                     name VARCHAR(128)
);
CREATE TABLE subject(
                        id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
                        name VARCHAR(128)
);
CREATE TABLE request(
                        id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
                        title VARCHAR(128),
                        description VARCHAR(1024),
                        price INT,
                        meetingType VARCHAR(128),
                        meetingSpecifications VARCHAR(128),
                        userId INT,
                        subjectId INT NOT NULL,
                        CONSTRAINT FK_REQUEST_USER FOREIGN KEY (userId) REFERENCES user(id),
                        CONSTRAINT FK_REQUEST_SUBJECT FOREIGN KEY (subjectId) REFERENCES subject(id)
);
CREATE TABLE offer(
                      id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
                      title VARCHAR(128),
                      description VARCHAR(1024),
                      price INT,
                      meetingType VARCHAR(128),
                      meetingSpecifications VARCHAR(128),
                      userId INT,
                      subjectId INT NOT NULL,
                      CONSTRAINT FK_OFFER_USER FOREIGN KEY (userId) REFERENCES user(id),
                      CONSTRAINT FK_OFFER_SUBJECT FOREIGN KEY (subjectId) REFERENCES subject(id)
);