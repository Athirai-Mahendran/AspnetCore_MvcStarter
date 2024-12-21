CREATE TABLE OTPRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    ContactValue VARCHAR(100) NOT NULL,  -- Email or Phone number
    ContactType VARCHAR(10) NOT NULL,    -- 'email' or 'phone'
    OTPCode VARCHAR(6) NOT NULL,         -- The generated OTP
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    ExpiresAt DATETIME NOT NULL,        -- OTP expiration timestamp
    IsVerified BIT DEFAULT 0,            -- Whether OTP was verified
    VerifiedAt DATETIME NULL,           -- When OTP was verified
    AttemptCount INT DEFAULT 0,          -- Number of verification attempts
    Status VARCHAR(20) DEFAULT 'PENDING' -- PENDING, VERIFIED, EXPIRED
   -- CONSTRAINT CK_ContactType CHECK (ContactType IN ('email', 'phone')),
   -- CONSTRAINT CK_Status CHECK (Status IN ('PENDING', 'VERIFIED', 'EXPIRED'))
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NULL UNIQUE,
    IsEmailVerified BIT NOT NULL DEFAULT 0,
    IsPhoneNumberVerified BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);
