import type { AxiosRequestConfig } from "axios";
import requestClient from "./request";

type HttpMethod = "CONNECT" | "DELETE" | "GET" | "HEAD" | "OPTIONS" | "PATCH" | "POST" | "PURGE" | "PUT" | "TRACE";

interface RequestConfig extends AxiosRequestConfig {
	method: HttpMethod;
}

export class RequestManager {
	private controllers = new Set<AbortController>();

	constructor() {
		this.request = this.request.bind(this); // 硬绑定
		this.cancel = this.cancel.bind(this);
	}

	request<T>(url: string, config: RequestConfig): Promise<T> {
		const controller = new AbortController();
		this.controllers.add(controller);

		return requestClient
			.request<T>(url, {
				...config,
				signal: controller.signal,
			})
			.finally(() => {
				this.controllers.delete(controller);
			});
	}

	// 取消所有请求
	cancel(message?: string) {
		this.controllers.forEach((controller) => controller.abort(message));
		this.controllers.clear();
	}
}

/*
tsx组件外:

const manager = new RequestManager();

manager
  .request('/api/data', { method: 'GET' })
  .then((data) => {
    console.log('Fetched data:', data);
  })
  .catch((error) => {
    console.error('Request failed:', error);
  });

// 手动取消请求
manager.cancel('Manually canceled!');

---

tsx组件内:
import React, { useEffect } from 'react';
import { useRequest } from './useRequest';

export default function MyComponent() {
  const requestManager = useRequest({ autoDestroy: true });

  useEffect(() => {
    requestManager
      .request('/api/data', { method: 'GET' })
      .then((data) => {
        console.log('Fetched data:', data);
      })
      .catch((error) => {
        console.error('Request failed:', error);
      });

    // 无需手动清理，组件卸载时自动清理
  }, [requestManager]);

  return <div>My React Component</div>;
}

*/
