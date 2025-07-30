// src/types.ts

/** 测试用例的期望值配置 */
export interface ExpectConfig {
  /** 期望的 HTTP 状态码 */
  status: number;
  /** 如果返回数据是数组，期望其最小长度 */
  minLength?: number;
}

/** 单个接口测试用例的定义 */
export interface TestCase {
  /** 用例名称，测试报告中显示 */
  name: string;
  /** HTTP 方法，如 GET、POST */
  method: string;
  /** 请求 URL */
  url: string;
  /** 期望值配置 */
  expect: ExpectConfig;
}
