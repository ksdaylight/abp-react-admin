BEGIN;
-- 创建Bookmarks表
CREATE TABLE "Bookmarks" (
    "Id" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "Hash" varchar(255) NOT NULL,
    "Model" text NOT NULL,
    "ModelType" text NOT NULL,
    "ActivityType" varchar(255) NOT NULL,
    "ActivityId" varchar(255) NOT NULL,
    "WorkflowInstanceId" varchar(255) NOT NULL,
    "CorrelationId" varchar(255),
    CONSTRAINT "PK_Bookmarks" PRIMARY KEY ("Id")
);

-- 创建WorkflowDefinitions表
CREATE TABLE "WorkflowDefinitions" (
    "Id" varchar(255) NOT NULL,
    "DefinitionId" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "Name" varchar(255),
    "DisplayName" text,
    "Description" text,
    "Version" integer NOT NULL,
    "IsSingleton" boolean NOT NULL,
    "PersistenceBehavior" integer NOT NULL,
    "DeleteCompletedInstances" boolean NOT NULL,
    "IsPublished" boolean NOT NULL,
    "IsLatest" boolean NOT NULL,
    "Tag" varchar(255),
    "Data" text,
    CONSTRAINT "PK_WorkflowDefinitions" PRIMARY KEY ("Id")
);

-- 创建WorkflowExecutionLogRecords表
CREATE TABLE "WorkflowExecutionLogRecords" (
    "Id" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "WorkflowInstanceId" varchar(255) NOT NULL,
    "ActivityId" varchar(255) NOT NULL,
    "ActivityType" varchar(255) NOT NULL,
    "Timestamp" timestamp(6) NOT NULL,
    "EventName" text,
    "Message" text,
    "Source" text,
    "Data" text,
    CONSTRAINT "PK_WorkflowExecutionLogRecords" PRIMARY KEY ("Id")
);

-- 创建WorkflowInstances表
CREATE TABLE "WorkflowInstances" (
    "Id" varchar(255) NOT NULL,
    "DefinitionId" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "Version" integer NOT NULL,
    "WorkflowStatus" integer NOT NULL,
    "CorrelationId" varchar(255),
    "ContextType" varchar(255),
    "ContextId" varchar(255),
    "Name" varchar(255),
    "CreatedAt" timestamp(6) NOT NULL,
    "LastExecutedAt" timestamp(6),
    "FinishedAt" timestamp(6),
    "CancelledAt" timestamp(6),
    "FaultedAt" timestamp(6),
    "Data" text,
    CONSTRAINT "PK_WorkflowInstances" PRIMARY KEY ("Id")
);

-- 创建索引
CREATE INDEX "IX_Bookmark_ActivityId" ON "Bookmarks" ("ActivityId");
CREATE INDEX "IX_Bookmark_ActivityType" ON "Bookmarks" ("ActivityType");
CREATE INDEX "IX_Bookmark_ActivityType_TenantId_Hash" ON "Bookmarks" ("ActivityType", "TenantId", "Hash");
CREATE INDEX "IX_Bookmark_CorrelationId" ON "Bookmarks" ("CorrelationId");
CREATE INDEX "IX_Bookmark_Hash" ON "Bookmarks" ("Hash");
CREATE INDEX "IX_Bookmark_Hash_CorrelationId_TenantId" ON "Bookmarks" ("Hash", "CorrelationId", "TenantId");
CREATE INDEX "IX_Bookmark_TenantId" ON "Bookmarks" ("TenantId");
CREATE INDEX "IX_Bookmark_WorkflowInstanceId" ON "Bookmarks" ("WorkflowInstanceId");

CREATE UNIQUE INDEX "IX_WorkflowDefinition_DefinitionId_VersionId" ON "WorkflowDefinitions" ("DefinitionId", "Version");
CREATE INDEX "IX_WorkflowDefinition_IsLatest" ON "WorkflowDefinitions" ("IsLatest");
CREATE INDEX "IX_WorkflowDefinition_IsPublished" ON "WorkflowDefinitions" ("IsPublished");
CREATE INDEX "IX_WorkflowDefinition_Name" ON "WorkflowDefinitions" ("Name");
CREATE INDEX "IX_WorkflowDefinition_Tag" ON "WorkflowDefinitions" ("Tag");
CREATE INDEX "IX_WorkflowDefinition_TenantId" ON "WorkflowDefinitions" ("TenantId");
CREATE INDEX "IX_WorkflowDefinition_Version" ON "WorkflowDefinitions" ("Version");

CREATE INDEX "IX_WorkflowExecutionLogRecord_ActivityId" ON "WorkflowExecutionLogRecords" ("ActivityId");
CREATE INDEX "IX_WorkflowExecutionLogRecord_ActivityType" ON "WorkflowExecutionLogRecords" ("ActivityType");
CREATE INDEX "IX_WorkflowExecutionLogRecord_TenantId" ON "WorkflowExecutionLogRecords" ("TenantId");
CREATE INDEX "IX_WorkflowExecutionLogRecord_Timestamp" ON "WorkflowExecutionLogRecords" ("Timestamp");
CREATE INDEX "IX_WorkflowExecutionLogRecord_WorkflowInstanceId" ON "WorkflowExecutionLogRecords" ("WorkflowInstanceId");

CREATE INDEX "IX_WorkflowInstance_ContextId" ON "WorkflowInstances" ("ContextId");
CREATE INDEX "IX_WorkflowInstance_ContextType" ON "WorkflowInstances" ("ContextType");
CREATE INDEX "IX_WorkflowInstance_CorrelationId" ON "WorkflowInstances" ("CorrelationId");
CREATE INDEX "IX_WorkflowInstance_CreatedAt" ON "WorkflowInstances" ("CreatedAt");
CREATE INDEX "IX_WorkflowInstance_DefinitionId" ON "WorkflowInstances" ("DefinitionId");
CREATE INDEX "IX_WorkflowInstance_FaultedAt" ON "WorkflowInstances" ("FaultedAt");
CREATE INDEX "IX_WorkflowInstance_FinishedAt" ON "WorkflowInstances" ("FinishedAt");
CREATE INDEX "IX_WorkflowInstance_LastExecutedAt" ON "WorkflowInstances" ("LastExecutedAt");
CREATE INDEX "IX_WorkflowInstance_Name" ON "WorkflowInstances" ("Name");
CREATE INDEX "IX_WorkflowInstance_TenantId" ON "WorkflowInstances" ("TenantId");
CREATE INDEX "IX_WorkflowInstance_WorkflowStatus" ON "WorkflowInstances" ("WorkflowStatus");
CREATE INDEX "IX_WorkflowInstance_WorkflowStatus_DefinitionId" ON "WorkflowInstances" ("WorkflowStatus", "DefinitionId");
CREATE INDEX "IX_WorkflowInstance_WorkflowStatus_DefinitionId_Version" ON "WorkflowInstances" ("WorkflowStatus", "DefinitionId", "Version");

-- 插入迁移历史记录
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210523093427_Initial', '5.0.10');

COMMIT;


-- 更新表
BEGIN;

ALTER TABLE "WorkflowInstances" ALTER COLUMN "CorrelationId" SET DEFAULT '';
ALTER TABLE "WorkflowInstances" ADD COLUMN "LastExecutedActivityId" text;
ALTER TABLE "WorkflowDefinitions" ADD COLUMN "OutputStorageProviderName" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210611200027_Update21', '5.0.10');

COMMIT;

-- 删除列和重命名表
BEGIN;

ALTER TABLE "WorkflowDefinitions" DROP COLUMN "OutputStorageProviderName";
-- ALTER TABLE "WorkflowInstances" RENAME TO "WorkflowInstances";
-- ALTER TABLE "WorkflowExecutionLogRecords" RENAME TO "WorkflowExecutionLogRecords";
-- ALTER TABLE "WorkflowDefinitions" RENAME TO "WorkflowDefinitions";
-- ALTER TABLE "Bookmarks" RENAME TO "Bookmarks";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210923112211_Update23', '5.0.10');

COMMIT;

-- 添加新列并修改列定义
BEGIN;

ALTER TABLE "WorkflowInstances" ADD COLUMN "DefinitionVersionId" text NOT NULL;
ALTER TABLE "Bookmarks" ALTER COLUMN "CorrelationId" SET DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211215100204_Update24', '5.0.10');

COMMIT;
-- 修改列定义并创建索引
BEGIN;

ALTER TABLE "WorkflowInstances" ALTER COLUMN "DefinitionVersionId" TYPE varchar(255) USING "DefinitionVersionId"::varchar;
CREATE INDEX "IX_WorkflowInstance_DefinitionVersionId" ON "WorkflowInstances" ("DefinitionVersionId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220120170050_Update241', '5.0.10');

COMMIT;

-- 创建Triggers表
BEGIN;

CREATE TABLE "Triggers" (
    "Id" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "Hash" varchar(255) NOT NULL,
    "Model" text NOT NULL,
    "ModelType" text NOT NULL,
    "ActivityType" varchar(255) NOT NULL,
    "ActivityId" varchar(255) NOT NULL,
    "WorkflowDefinitionId" varchar(255) NOT NULL,
    CONSTRAINT "PK_Triggers" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_Trigger_ActivityId" ON "Triggers" ("ActivityId");
CREATE INDEX "IX_Trigger_ActivityType" ON "Triggers" ("ActivityType");
CREATE INDEX "IX_Trigger_ActivityType_TenantId_Hash" ON "Triggers" ("ActivityType", "TenantId", "Hash");
CREATE INDEX "IX_Trigger_Hash" ON "Triggers" ("Hash");
CREATE INDEX "IX_Trigger_Hash_TenantId" ON "Triggers" ("Hash", "TenantId");
CREATE INDEX "IX_Trigger_TenantId" ON "Triggers" ("TenantId");
CREATE INDEX "IX_Trigger_WorkflowDefinitionId" ON "Triggers" ("WorkflowDefinitionId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220120204150_Update25', '5.0.10');

COMMIT;

-- 添加CreatedAt列
BEGIN;

ALTER TABLE "WorkflowDefinitions" ADD COLUMN "CreatedAt" timestamp(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220512203646_Update28', '5.0.10');

COMMIT;

-- 创建WebhookDefinitions表
BEGIN;

CREATE TABLE "WebhookDefinitions" (
    "Id" varchar(255) NOT NULL,
    "TenantId" varchar(255),
    "Name" varchar(255) NOT NULL,
    "Path" varchar(255) NOT NULL,
    "Description" varchar(255),
    "PayloadTypeName" varchar(255),
    "IsEnabled" boolean NOT NULL,
    CONSTRAINT "PK_WebhookDefinitions" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_WebhookDefinition_Description" ON "WebhookDefinitions" ("Description");
CREATE INDEX "IX_WebhookDefinition_IsEnabled" ON "WebhookDefinitions" ("IsEnabled");
CREATE INDEX "IX_WebhookDefinition_Name" ON "WebhookDefinitions" ("Name");
CREATE INDEX "IX_WebhookDefinition_Path" ON "WebhookDefinitions" ("Path");
CREATE INDEX "IX_WebhookDefinition_PayloadTypeName" ON "WebhookDefinitions" ("PayloadTypeName");
CREATE INDEX "IX_WebhookDefinition_TenantId" ON "WebhookDefinitions" ("TenantId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210604065041_Initial', '5.0.10');

COMMIT;

-- 创建WorkflowSettings表
BEGIN;

CREATE TABLE "WorkflowSettings" (
    "Id" varchar(255) NOT NULL,
    "WorkflowBlueprintId" varchar(255),
    "Key" varchar(255),
    "Value" varchar(255),
    CONSTRAINT "PK_WorkflowSettings" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_WorkflowSetting_Key" ON "WorkflowSettings" ("Key");
CREATE INDEX "IX_WorkflowSetting_Value" ON "WorkflowSettings" ("Value");
CREATE INDEX "IX_WorkflowSetting_WorkflowBlueprintId" ON "WorkflowSettings" ("WorkflowBlueprintId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210730112043_Initial', '5.0.10');

COMMIT;
