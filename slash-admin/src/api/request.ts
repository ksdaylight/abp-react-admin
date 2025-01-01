import { authenticateResponseInterceptor, errorMessageResponseInterceptor, RequestClient } from "@/request-client";
import userStore, { useUserToken } from "@/store/userStore";
import { toast } from "sonner";

  const requestClient = new RequestClient({
    baseURL:import.meta.env.VITE_APP_BASE_API
  });

   /**
   * 重新认证逻辑
   */
  async function doReAuthenticate() {
    console.warn('Access token or refresh token is invalid or expired. ');
    //直接登出
    userStore.getState().actions.clearUserInfoAndToken();
  }

    /**
   * 刷新token逻辑
   */
  async function doRefreshToken() {
    return ''; //TODO 
  }

  function formatToken(token: null | string) {
    return token ? `Bearer ${token}` : null; //TODO 可能需要调短过期时间来验证下
  }

    // 请求头处理
  requestClient.addRequestInterceptor({
    fulfilled: async (config) => {
      const { accessToken } = useUserToken();
      if (accessToken) {
        config.headers.Authorization = `${accessToken}`;
      }
      config.headers['Accept-Language'] ='zh-CN'; //'en-US' | 'zh-CN'; //TODO 适配设置
      config.headers['X-Request-From'] = 'slash-admin';
      return config;
    },
  });


  // response数据解构
  requestClient.addResponseInterceptor<any>({
    fulfilled: (response) => {
      const { data, status, headers } = response;

      if (headers._abpwrapresult === 'true') {
        const { code, result, message, details } = data;
        const hasSuccess = data && Reflect.has(data, 'code') && code === '0';
        if (hasSuccess) {
          return result;
        }
        const content = details || message;

        throw Object.assign({}, response, { response, message: content });
      }

      if (status >= 200 && status < 400) {
        return data;
      }

      throw Object.assign({}, response, { response });
    },
  });

  // token过期的处理
  requestClient.addResponseInterceptor(
    authenticateResponseInterceptor({
      client: requestClient,
      doReAuthenticate,
      doRefreshToken,
      enableRefreshToken: true,//TODO 
      formatToken,
    }),
  );

  // 通用的错误处理,如果没有进入上面的错误处理逻辑，就会进入这里
  requestClient.addResponseInterceptor(
    errorMessageResponseInterceptor((msg: string, error) => {
      // 这里可以根据业务进行定制,你可以拿到 error 内的信息进行定制化处理，根据不同的 code 做不同的提示，而不是直接使用 message.error 提示 msg
      // 当前mock接口返回的错误字段是 error 或者 message
      const responseData = error?.response?.data ?? {};
      const errorMessage = responseData?.error ?? responseData?.message ?? '';
      // 如果没有错误信息，则会根据状态码进行提示
      toast.error(errorMessage || msg, {
			  position: "top-center",
		    });
    }),
  );

  export default requestClient;