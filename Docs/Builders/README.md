## Linear builders

Let's try to come up with a DSL so that we specify our desired api

```ts
interface ConstructionProposal {
    readonly path: List<Point>;
    readonly cancel: () => void;
    readonly changes: Map<Point, [string,  string]>; // associate with every point a tuple of the proposed change
}
interface BuilderSpec<T extends string> {
    name: T;  //  road,  pipe, powerline,  etc
    ghost: string; // refers  to some external  fbx to use  as ghost during  building
    canBuild(currentTile: T): boolean;
    onBeforeBuild?: (reviewFn: (proposal: ConstructionProposal) => void)  =>  void;
    repairer?: {
        lift: (tile:  T) => Ap<T>; // tell us where else to look
        fix:  (tile: T, mT: Ap<T>)  => Ap<T>; // do the repair
    };
    onAfterBuild?: (reviewFn: (proposal: ConstructionProposal) =>  void) =>  void;
}

```


## Box builders

These are for building "walls"

```ts
interface BuilderSpec  {
    name: T;
    ghost: string;
    canBuild(ptA: Point, orientation: 'horizontal'  |  'vertical' | 'diagonal'): boolean;
    onBeforeBuild?: (reviewFn: (proposal: ConstructionProposal) => void)  =>  void;
    onAfterBuild?: (reviewFn: (proposal: ConstructionProposal) =>  void) =>  void;
}
```

## Field Builders

```ts
interface BuilderSpec {
    name: string;
    ghost: string;
    canBuild;
    discoverBoundaries(pt: Point): Boundary<Point>;
}
```