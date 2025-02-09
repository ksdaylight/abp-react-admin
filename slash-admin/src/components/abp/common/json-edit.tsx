import type React from "react";
import { JsonEditor, githubDarkTheme, githubLightTheme } from "json-edit-react";
import { useTheme } from "@/theme/hooks";
import { ThemeMode } from "#/enum";

interface JsonEditProps {
	data: object | any[];
}
/**
 * default readonly json viewer
 */
const JsonEdit: React.FC<JsonEditProps> = ({ data }) => {
	const { mode } = useTheme();

	return (
		<JsonEditor
			rootName={""}
			data={data}
			theme={mode === ThemeMode.Dark ? githubDarkTheme : githubLightTheme}
			restrictEdit={true}
			restrictDelete={true}
			restrictAdd={true}
		/>
	);
};

export default JsonEdit;
