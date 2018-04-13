import { ModelDeclaration } from "./types";
import { exec } from 'child_process';
import { parseTypescriptModel } from "./tsModelParser";
const config = require('./config.json') as Config;

export class Commands {
	// private dotNetProcess: string = "C:\\GIT\\hakaton042018\\netApp\\ClassParser\\ClassParser\\bin\\Debug\\ClassParser.exe C:\\GIT\\hakaton042018\\netApp\\ClassToParse\\ActivityViewModel.cs";
	public sync = () => console.log('not implemented');

	public diff = () => console.log('not implemented');

	public read = (filePath: string): ModelDeclaration[] => {
		let cmd = `${config.dotNetProcess} ${config.root}${config.dotNetModel}${filePath}`;
		this.invoke(cmd).then(() => { });
		return null;
	};

	private invoke = (cmd: string): Promise<ModelDeclaration> => {
		return new Promise<ModelDeclaration>((resolve: any, reject: any) => {
			exec(cmd, (error: any, stdout: any, _stderr: any) => {
				if (error) {
					console.error(`exec error: ${error}`);
					reject();
				}

				resolve(<ModelDeclaration>JSON.parse(stdout));
			});
		})
	}
}

type Config = {
	root: string;
	dotNetProcess: string
	dotNetModel: string;
	dtoModel: string;
	tsModel: string;
};