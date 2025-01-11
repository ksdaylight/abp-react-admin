import { useEffect, useMemo } from 'react';
import { RequestManager } from './request-manager';

interface RequestLifeCycle {
  /** 是否自动销毁令牌 */
  autoDestroy?: boolean;
}

export function useRequest(options?: RequestLifeCycle) {
  // 创建 RequestManager 实例
  const requestManager = useMemo(() => new RequestManager(), []);

  useEffect(() => {
    return () => {
      if (options?.autoDestroy !== false) {
        requestManager.cancel('The Component has Unmounted!');
      }
    };
  }, [options, requestManager]);

  return requestManager;
}
