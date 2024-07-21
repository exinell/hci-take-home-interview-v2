export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
    totalRecords?: number;
}