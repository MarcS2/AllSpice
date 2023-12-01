CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT PRIMARY KEY,
        title CHAR(255) NOT NULL,
        instructions CHAR(255) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        category CHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT AUTO_INCREMENT PRIMARY KEY,
        name CHAR(255) NOT NULL,
        quantity VARCHAR(150) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (id) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';