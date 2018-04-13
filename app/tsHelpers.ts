import * as fs from 'async-file';
import * as path from 'path';
import ts from 'typescript';

export async function openTypescriptFile(filePath: string) {
    const sourceText = await fs.readTextFile(filePath, 'utf8');
    return ts.createSourceFile(path.basename(filePath), sourceText, ts.ScriptTarget.Latest, false, ts.ScriptKind.TS);
}

export function findFirstNode(node: ts.Node, kind: ts.SyntaxKind[]) {
    let result: ts.Node;

    node.forEachChild(visitChild);

    return result;

    function visitChild(child: ts.Node) {
        if (kind.some(k => child.kind == k)) {
            result = child;
            return;
        }

        child.forEachChild(visitChild);
    }
}

export const SyntaxKindToStringTypeMapping = new Map<ts.SyntaxKind, string>([
    [ts.SyntaxKind.StringKeyword, 'string'],
    [ts.SyntaxKind.NumberKeyword, 'number'],
    [ts.SyntaxKind.BooleanKeyword, 'boolean'],
    [ts.SyntaxKind.AnyKeyword, 'any'],
]);

const reverseMap = new Map<string, ts.SyntaxKind>();
SyntaxKindToStringTypeMapping.forEach((value, key) => reverseMap.set(value, key));

export const StringToSyntaxKindTypeMapping = reverseMap;