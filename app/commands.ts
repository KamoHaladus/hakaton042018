import { ModelDeclaration, Config, Content, UpdateModelCommand } from "./types";
import columnify from 'columnify';
import { exec } from 'child_process';
import globby from 'globby';
import { parseTypescriptModel } from "./tsModelParser";
import _ from 'lodash';
import { updateModel } from "./tsModelUpdater";
import * as fs from "fs";
import * as path from "path";
const config = require('./config.json') as Config;

export class Commands {

	public sync = async (modelName: string) => {
		let content: Content = await this.read(modelName);
		let add = _.differenceWith(content.dotNetModelContent.members, content.dtoModelContent.members, (dotNet, dto) => dotNet.name.toLowerCase() == dto.name.toLowerCase());
		let remove = _.differenceWith(content.dtoModelContent.members, content.dotNetModelContent.members, (dto, dotNet) => dto.name.toLowerCase() == dotNet.name.toLowerCase());
		let cmd: UpdateModelCommand = { add: add, modelName: modelName, remove: remove.map(r => r.name) };
		let result = await updateModel(content.modelPaths.dtoModel, cmd);
		console.log(result);
	}

	public watch = () => {
		fs.watch(path.join(config.root, config.dotNetModel), { recursive: true }, (event, filename) => {
			if (!filename.endsWith("ViewModel.cs") || event != "change") {
				return;
			}

			const modelName = path.basename(filename, '.cs');

			this.diff(modelName);
		});
	}

	public diff = async (modelName: string) => {
		let content: Content = await this.read(modelName);
		let add = _.differenceWith(content.dotNetModelContent.members, content.dtoModelContent.members, (dotNet, dto) => dotNet.name.toLowerCase() == dto.name.toLowerCase());
		let remove = _.differenceWith(content.dtoModelContent.members, content.dotNetModelContent.members, (dto, dotNet) => dto.name.toLowerCase() == dotNet.name.toLowerCase());
		this.printDiff({
			add: add.map((m) => (`${m.name}: ${m.type.name};`)),
			space: ["      "],
			remove: remove.map((m) => (`${m.name}: ${m.type.name};`))
		});
	}

	public async read(modelName: string): Promise<Content> {
		let modelPaths = await Promise.all([this.setGlobe(modelName, config.dotNetModel), this.setGlobe(modelName, config.dtoModel)])
			.then((result: [string[], string[]]) => {
				return {
					dotNetModel: `${config.root}${config.dotNetModel}${result[0][0]}`,
					dtoModel: `${config.root}${config.dtoModel}${result[1][0]}`
				};
			});
		return await Promise
			.all([
				this.invoke(`${config.dotNetProcess} ${modelPaths.dotNetModel}`),
				parseTypescriptModel(modelPaths.dtoModel)
			]).then((result: [ModelDeclaration, ModelDeclaration]) => {
				return {
					dotNetModelContent: result[0],
					dtoModelContent: result[1],
					modelPaths
				};
			});
	}

	private printDiff(data) {
		let cols = columnify([data], { minWidth: 20, align: "left", config: { add: { maxWidth: 70 } } });
		console.log(cols);
	}

	private setGlobe(fileName: string, modelPath: string): Promise<string[]> {
		let cwd = `${config.root}${modelPath}`;
		return globby(`**/${fileName}{.cs,.d.ts}`, <any>{ cwd, case: false });
	}

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

