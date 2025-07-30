// src/testRunner.test.ts

// 用 CommonJS 方式引入 axios 和 tests.json，得到 any 类型
const axios = require('axios');
const tests = require('../tests.json');

describe('API 自动化测试', () => {
  // tests 已经是一个数组，每个元素就是你 tests.json 里定义的对象
  tests.forEach((tc: { name: string; method: string; url: string; expect: { status: number; minLength?: number } }) => {
    test(tc.name, async () => {
      // 发起请求
      const response = await axios.request({
        method: tc.method,
        url: tc.url,
      });

      // 校验状态码
      expect(response.status).toBe(tc.expect.status);

      // 如果配置了 minLength，就校验返回值是数组且长度 OK
      if (tc.expect.minLength !== undefined) {
        expect(Array.isArray(response.data)).toBe(true);
        expect(response.data.length).toBeGreaterThanOrEqual(tc.expect.minLength);
      }
    });
  });
});
