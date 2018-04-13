export type ModelDeclaration = {
    name: string;
    members: MemberDeclaration[];
}

export type MemberDeclaration = {
    name: string,
    type: TypeDeclaration
}

export type TypeDeclaration = {
    name: string,
    isArray?: boolean;
    isNullable?: boolean;
}

export type Content = {
    dotNetModelContent: ModelDeclaration,
    dtoModelContent: ModelDeclaration,
    modelPaths: { dotNetModel: string, dtoModel: string }
};

export type Config = {
    root: string;
    dotNetProcess: string
    dotNetModel: string;
    dtoModel: string;
    tsModel: string;
};
export type UpdateModelCommand = {
    modelName: string,
    add: MemberDeclaration[],
    remove: string[]
}
