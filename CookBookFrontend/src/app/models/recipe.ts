export class RecipeShortInfoResponceDto {
    constructor(
        public id: number,
        public title: string,
        public shortDescription: string,
        public preparingTime: Number,
        public personCount: Number,
        public tags: string[],
        public likesCount: Number,
        public favoritesCount: Number,
        public authorId: Number,
        public authorName: string) { }
}

export class RecipeFullInfoResponceDto {
    public recipeShortInfo: RecipeShortInfoResponceDto;
    public cookingSteps: string[];
    public recipeIngridients: RecipeIngredient[];
}

export class RecipeIngredient {
    public title: string;
    public ingredients: string[];

    constructor() {
        this.ingredients = [];
    }
}

export class AddRecipeRequestDto {
    public title: string;
    public shortDescription: string;
    public preparingTime: Number;
    public personCount: Number;
    public tags: string[];
    public cookingSteps: string[];
    public recipeIngridients: RecipeIngredient[];

    constructor() {
        this.tags = [];
        this.cookingSteps = [];
        this.recipeIngridients = [];
    }
}

