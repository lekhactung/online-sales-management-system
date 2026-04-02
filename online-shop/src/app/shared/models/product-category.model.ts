export interface ProductCategory {
  categoryId: string;
  categoryName: string;
  description?: string;
}

export interface CreateProductCategory {
  categoryName: string;
  description?: string;
}
