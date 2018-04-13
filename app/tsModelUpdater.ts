import { UpdateModelCommand, MemberDeclaration, TypeDeclaration } from "./types";
import { openTypescriptFile, findFirstNode, StringToSyntaxKindTypeMapping } from "./tsHelpers";
import ts, { TypeElement, TypeNode, ClassElement } from 'typescript';

export async function updateModel(modelPath: string, updateCommand: UpdateModelCommand): Promise<string> {
    const ast = await openTypescriptFile(modelPath);

    const node = findFirstNode(ast, [ts.SyntaxKind.InterfaceDeclaration, ts.SyntaxKind.ClassDeclaration]);
    if (node.kind == ts.SyntaxKind.InterfaceDeclaration) {
        updateInterfaceMembers(node, updateCommand);
    } else if (node.kind == ts.SyntaxKind.ClassDeclaration) {
        updateClassMembers(node, updateCommand);
    }

    const printer = ts.createPrinter();
    const result = printer.printFile(ast);

    return result;
}

function updateClassMembers(node: ts.Node, updateCommand: UpdateModelCommand) {
    const classDecl = node as ts.ClassDeclaration;
    classDecl.members= ts.createNodeArray(classDecl.members.filter(m => {
        if (m.kind != ts.SyntaxKind.PropertyDeclaration) {
            return true; // leave all other things untouched
        }

        const ps = m as ts.PropertyDeclaration;
        const identifier = ps.name as ts.Identifier;

        return updateCommand.remove.every(toRemove => identifier.text != toRemove);
    }).concat(updateCommand.add.map(createPropertyDeclaration)));
}

function createPropertyDeclaration(memberDeclaration: MemberDeclaration): ClassElement {
    return ts.createProperty(
        [],
        [],
        memberDeclaration.name,
        undefined,
        createTypeNode(memberDeclaration.type),
        undefined);
}

function updateInterfaceMembers(node: ts.Node, updateCommand: UpdateModelCommand) {
    const interfaceDecl = node as ts.InterfaceDeclaration;
    interfaceDecl.members = ts.createNodeArray(interfaceDecl.members.filter(m => {
        if (m.kind != ts.SyntaxKind.PropertySignature) {
            return true; // leave all other things untouched
        }

        const ps = m as ts.PropertySignature;
        const identifier = ps.name as ts.Identifier;

        return updateCommand.remove.every(toRemove => identifier.text != toRemove);
    }).concat(updateCommand.add.map(createPropertySignature)));
}

function createPropertySignature(memberDeclaration: MemberDeclaration): TypeElement {
    return ts.createPropertySignature(
        [],
        memberDeclaration.name,
        undefined,
        createTypeNode(memberDeclaration.type),
        undefined);
}

function createTypeNode(type: TypeDeclaration): TypeNode {
    const mappedType: ts.SyntaxKind = StringToSyntaxKindTypeMapping.get(type.name) || ts.SyntaxKind.AnyKeyword;

    return ts.createKeywordTypeNode(mappedType as ts.KeywordTypeNode["kind"]);
}