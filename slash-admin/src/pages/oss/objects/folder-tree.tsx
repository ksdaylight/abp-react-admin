import React, { useState, useEffect } from "react";
import { Card, Select, Button, Tree, Empty } from "antd";
import { useTranslation } from "react-i18next";
import { getListApi as getContainersApi, getObjectsApi } from "@/api/oss/containes";
import type { OssContainerDto } from "#/oss/containes";
import FolderModal from "./folder-modal";

const { DirectoryTree } = Tree;

interface FolderNode {
  key: string;
  title: string;
  isLeaf?: boolean;
  children?: FolderNode[];
  dataRef?: any;
}

interface Props {
  onBucketChange: (bucket: string) => void;
  onFolderChange: (path: string) => void;
}

const FolderTree: React.FC<Props> = ({ onBucketChange, onFolderChange }) => {
  const { t: $t } = useTranslation();
  const [bucket, setBucket] = useState<string>("");
  const [containers, setContainers] = useState<OssContainerDto[]>([]);
  const [treeData, setTreeData] = useState<FolderNode[]>([]);
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedPathForCreate, setSelectedPathForCreate] = useState<string>("");

  useEffect(() => {
    initContainers();
  }, []);

  const initContainers = async () => {
    const res = await getContainersApi({ maxResultCount: 1000 });
    setContainers(res.containers);
  };

  const handleBucketChange = (val: string) => {
    setBucket(val);
    onBucketChange(val);
    setTreeData([
      {
        key: "./",
        title: $t("AbpOssManagement.Objects:Root"),
        isLeaf: false,
        dataRef: { path: "", name: "" },
      },
    ]);
  };

  const getFolders = async (bucketName: string, prefix: string) => {
    const { objects } = await getObjectsApi({
      bucket: bucketName,
      delimiter: "/",
      maxResultCount: 1000,
      prefix: prefix,
    });
    return objects
      .filter((f) => f.isFolder)
      .map((folder) => ({
        key: `${folder.path || ""}${folder.name}`,
        title: folder.name,
        isLeaf: false, // Assume folders might have children
        dataRef: folder,
      }));
  };

  const onLoadData = async ({ key, children, dataRef }: any) => {
    if (children && children.length > 0) return;
    
    let path = "";
    if (dataRef?.path) path += dataRef.path;
    if (dataRef?.name && dataRef.name !== "./") path += dataRef.name;

    try {
        const childFolders = await getFolders(bucket, path);
        setTreeData((origin) => updateTreeData(origin, key, childFolders));
    } catch {
        setTreeData((origin) => updateTreeData(origin, key, []));
    }
  };

  const updateTreeData = (list: FolderNode[], key: React.Key, children: FolderNode[]): FolderNode[] => {
    return list.map((node) => {
      if (node.key === key) {
        return { ...node, children };
      }
      if (node.children) {
        return { ...node, children: updateTreeData(node.children, key, children) };
      }
      return node;
    });
  };

  const onSelect = (keys: React.Key[], info: any) => {
    if (keys.length === 1) {
      const keyStr = keys[0].toString();
      onFolderChange(keyStr);
      setSelectedPathForCreate(keyStr === "./" ? "" : keyStr);
    }
  };

  const handleCreateFolder = () => {
    setModalVisible(true);
  };

  const handleFolderCreated = () => {
      // Refresh logic could be complex here (reloading specific node), 
      // for simplicity we might just reload the bucket or let user expand/collapse
      // Ideally, trigger a reload of the parent node of the created folder.
  };

  return (
    <>
      <Card title={$t("AbpOssManagement.Containers")} className="h-full">
        <div className="flex flex-col gap-2">
          <Select
            placeholder={$t("AbpOssManagement.Containers:Select")}
            options={containers.map(c => ({ label: c.name, value: c.name }))}
            value={bucket || undefined}
            onChange={handleBucketChange}
            className="w-full"
          />
          {bucket ? (
            <>
              <Button block type="primary" ghost onClick={handleCreateFolder}>
                {$t("AbpOssManagement.Objects:CreateFolder")}
              </Button>
              <DirectoryTree
                loadData={onLoadData}
                treeData={treeData}
                onSelect={onSelect}
                defaultExpandedKeys={["./"]}
              />
            </>
          ) : (
            <Empty />
          )}
        </div>
      </Card>
      <FolderModal
        visible={modalVisible}
        bucket={bucket}
        path={selectedPathForCreate}
        onClose={() => setModalVisible(false)}
        onChange={handleFolderCreated}
      />
    </>
  );
};

export default FolderTree;