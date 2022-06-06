export class RecipeShortInfoResponceDto {
    constructor(
        public id: Number,
        public title: string,
        public shortDescription: string,
        public preparingTime: Number,
        public tags: string[],
        public likesCount: Number,
        public favoritesCount: Number,
        public authorId: Number,
        public authorName : string) { }
}

export class RecipeIngridient {
    public ingridientTitle: string;
    public ingridientBody: string;
}

export class AddRecipeRequestDto {
    public title: string;
    public shortDescription: string;
    public preparingTime: Number;
    public personCount: Number;
    public tags: string[];
    public cookingSteps: string[];
    public recipeIngridients: RecipeIngridient[];

    constructor() {
        this.tags = [];
        this.cookingSteps = [];
        this.recipeIngridients = [];
    }
}

