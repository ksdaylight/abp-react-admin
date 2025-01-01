// react-query
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
// vercel analytics
import { Analytics } from "@vercel/analytics/react";
// react
import { Suspense } from "react";
import ReactDOM from "react-dom/client";
// helmet
import { HelmetProvider } from "react-helmet-async";
//
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import "virtual:svg-icons-register";
// mock api
import worker from "./_mock";
// i18n
import "./locales/i18n";
// css
import "./global.css";
import "./theme/theme.css";

// root component
import App from "./App";

const charAt = `
    ███████╗██╗      █████╗ ███████╗██╗  ██╗ 
    ██╔════╝██║     ██╔══██╗██╔════╝██║  ██║
    ███████╗██║     ███████║███████╗███████║
    ╚════██║██║     ██╔══██║╚════██║██╔══██║
    ███████║███████╗██║  ██║███████║██║  ██║
    ╚══════╝╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
  `;
console.info(`%c${charAt}`, "color: #5BE49B");

const queryClient = new QueryClient({
	defaultOptions: {
		queries: {
			retry: 3,
			gcTime: 300_000,
			staleTime: 10_1000,
			refetchOnWindowFocus: false,
			refetchOnReconnect: false,
			refetchOnMount: false,
		},
	},
});

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);

root.render(
	<HelmetProvider>
		<QueryClientProvider client={queryClient}>
			<ReactQueryDevtools initialIsOpen={false} />
			<Suspense>
				<Analytics />

				<App />
			</Suspense>
		</QueryClientProvider>
	</HelmetProvider>,
);

// 🥵 start service worker mock in development mode
// worker.start({ onUnhandledRequest: "bypass" });
