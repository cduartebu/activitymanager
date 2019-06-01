export class ResponseDto<T> {
    StatusCode: number;
    StatusMessage: string;
    Data: T;
}